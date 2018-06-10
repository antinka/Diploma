using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Builder.Implementation;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GameStore.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        private readonly FilterViewModelBuilder _filterViewModelBuilder;

        public GameController(
            IGameService gameService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IMapper mapper,
            IPublisherService publisherService,
            FilterViewModelBuilder filterViewModelBuilder)
            IAuthentication authentication) : base(authentication)
        {
            _gameService = gameService;
            _mapper = mapper;
            _publisherService = publisherService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _filterViewModelBuilder = filterViewModelBuilder;
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(GetGameViewModelForCreate(new GameViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(GameViewModel game)
        {
            game = CheckValidationGameViewModel(game);

            if (ModelState.IsValid)
            {
                var gameDTO = _mapper.Map<ExtendGameDTO>(game);
                _gameService.AddNew(gameDTO);

                return RedirectToAction("FilteredGames");
            }

            return View(GetGameViewModelForCreate(game));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Update(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<GameViewModel>(gameDTO);

            gameForView = GetGameViewModelForUpdate(gameForView);

            return View(gameForView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Update(GameViewModel game)
        {
            game = CheckValidationGameViewModel(game);

            if (ModelState.IsValid)
            {
                var gameExtendGameDto = _mapper.Map<ExtendGameDTO>(game);
                _gameService.Update(gameExtendGameDto);

                return RedirectToAction("FilteredGames");
            }

            return View(GetGameViewModelForUpdate(game));
        }

        [HttpGet]
        public ActionResult GamesFilters(FilterViewModel filterViewModel)
        {
            var model = _filterViewModelBuilder.Rebuild(filterViewModel);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult FilteredGames(FilterViewModel filterViewModel, int page = 1)
        {
            if (filterViewModel.MinPrice > filterViewModel.MaxPrice)
            {
                ModelState.AddModelError("MinPrice", GlobalRes.MinMaxPrice);
            }

            if (filterViewModel.PageSize == 0)
            {
                filterViewModel.PageSize = PageSize.Ten;
            }

            var gamesByFilter = _gameService.GetGamesByFilter(_mapper.Map<FilterDTO>(filterViewModel), page, filterViewModel.PageSize, out var totalItemsByFilter);

            int totalItem = 0;

            if (filterViewModel.PageSize == PageSize.All)
            {
                totalItem = _gameService.GetCountGame();
            }
            else
            {
                totalItem = (int)filterViewModel.PageSize;
            }

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = totalItem,
                TotalItemsByFilter = totalItemsByFilter
            };

            filterViewModel.PagingInfo = pagingInfo;

            filterViewModel = _filterViewModelBuilder.Rebuild(filterViewModel);

            if (filterViewModel.PageSize == 0)
            {
                filterViewModel.PageSize = PageSize.Ten;
            }

            var gameViewModel = _mapper.Map<IEnumerable<DetailsGameViewModel>>(gamesByFilter);

            if (gameViewModel.Any())
            {
                filterViewModel.Games = gameViewModel;
            }
            else
            {
                filterViewModel.Games = new List<DetailsGameViewModel>() { new DetailsGameViewModel() };
            }

            return View(filterViewModel);
        }

        [HttpGet]
        public ActionResult GetGame(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<DetailsGameViewModel>(gameDTO);

            return View(gameForView);
        }

        [HttpGet]
        public ActionResult GetAllGames()
        {
            var gamesDTO = _gameService.GetAll();
            var gamesForView = _mapper.Map<IEnumerable<DetailsGameViewModel>>(gamesDTO);

            return View(gamesForView);
        }

        [HttpPost]
        public ActionResult Remove(Guid gameId)
        {
            _gameService.Delete(gameId);

            return PartialView("GameDeleted");
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult Download(string gamekey)
        {
            var path = Server.MapPath("~/Files/test.pdf");
            var mas = System.IO.File.ReadAllBytes(path);

            return new FileContentResult(mas, "application/pdf");
        }

        [OutputCache(Duration = 60)]
        public ActionResult CountGames()
        {
            var gameCount = _gameService.GetCountGame();

            return PartialView("CountGames", gameCount);
        }

        private GameViewModel CheckValidationGameViewModel(GameViewModel game)
        {
            var gameExtendGameDto = _mapper.Map<ExtendGameDTO>(game);

            if (game.SelectedPlatformTypesName == null)
            {
                ModelState.AddModelError("PlatformTypes", GlobalRes.ChoosePlatformTypes);
            }

            if (!_gameService.IsUniqueKey(gameExtendGameDto))
            {
                ModelState.AddModelError("Key", GlobalRes.ExistKey);
            }

            return game;
        }

        private GameViewModel CreateCheckBoxForGameViewModel(GameViewModel gameViewModel)
        {
            var genrelist = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(_genreService.GetAll());
            var platformlist = _mapper.Map<IEnumerable<DetailsPlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publishers = _mapper.Map<IEnumerable<DetailsPublisherViewModel>>(_publisherService.GetAll());

            gameViewModel.PublisherList = new SelectList(publishers, "Id", "Name");

            var listGenreBoxes = new List<CheckBox>();
            genrelist.Select(genre => { listGenreBoxes.Add(new CheckBox() { Text = genre.Name }); return genre; }).ToList();
            gameViewModel.ListGenres = listGenreBoxes;

            var listPlatformBoxes = new List<CheckBox>();
            platformlist.Select(platform => { listPlatformBoxes.Add(new CheckBox() { Text = platform.Name }); return platform; }).ToList();
            gameViewModel.ListPlatformTypes = listPlatformBoxes;

            return gameViewModel;
        }

        private GameViewModel GetGameViewModelForCreate(GameViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForGameViewModel(gameViewModel);

            if (gameViewModel.SelectedPlatformTypesName != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes.Where(x => gameViewModel.SelectedPlatformTypesName.Contains(x.Text));
            }

            if (gameViewModel.SelectedGenresName != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres.Where(x => gameViewModel.SelectedGenresName.Contains(x.Text));
            }

            return gameViewModel;
        }

        private GameViewModel GetGameViewModelForUpdate(GameViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForGameViewModel(gameViewModel);
            gameViewModel = GetGameViewModelForCreate(gameViewModel);

            if (gameViewModel.PlatformTypes != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes.Where(x => gameViewModel.PlatformTypes.Any(g => g.Name.Contains(x.Text)));
            }

            if (gameViewModel.Genres != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres.Where(x => gameViewModel.Genres.Any(g => g.Name.Contains(x.Text)));
            }

            return gameViewModel;
        }
    }
}