using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Builder.Implementation;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Games;
using System.Net;
using GameStore.Web.Infrastructure;

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
            FilterViewModelBuilder filterViewModelBuilder,
            IAuthentication authentication) : base(authentication)
        {
            _gameService = gameService;
            _mapper = mapper;
            _publisherService = publisherService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _filterViewModelBuilder = filterViewModelBuilder;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult New()
        {
            return View(GetGameViewModelForCreate(new GameViewModel()));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(GameViewModel game, HttpPostedFileBase image)
        {
            game = CheckValidationGameViewModel(game);

            if (ModelState.IsValid)
            {
                var gameDTO = _mapper.Map<ExtendGameDTO>(game);

                if (image != null)
                {
                    var pictureName = gameDTO.Key + "_" + image.FileName;
                    image.SaveAs(Server.MapPath($"~/Content/Images/Games/{pictureName}"));
                    gameDTO.ImageName = pictureName;
                    gameDTO.ImageMimeType = image.ContentType;
                }
                else
                {
                    gameDTO.ImageName = "Game-Zone.png";
                    gameDTO.ImageMimeType = "image/png";
                }

                _gameService.AddNew(gameDTO);

                return RedirectToAction("FilteredGames");
            }

            return View(GetGameViewModelForCreate(game));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Update(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);

            if (!gameDTO.IsDelete)
            {
                var gameForView = _mapper.Map<GameViewModel>(gameDTO);

                gameForView = GetGameViewModelForUpdate(gameForView);

                return View(gameForView);
            }

            return RedirectToAction("FilteredGames");
        }

        [HttpGet]
        [Authorize(Roles = "Publisher")]
        public ActionResult UpdateByPublisher(string gamekey)
        {
            var gameDTO = _gameService.GetGameByPublisherIdAndGameKey(CurrentUser.Id, gamekey);

            if (!gameDTO.IsDelete)
            {
                var gameForView = _mapper.Map<GameViewModel>(gameDTO);

                gameForView = GetGameViewModelForUpdate(gameForView);

                return View("Update", gameForView);
            }

            return RedirectToAction("FilteredGames");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Publisher")]
        public ActionResult Update(GameViewModel game, HttpPostedFileBase image)
        {
            game = CheckValidationGameViewModel(game);

            if (ModelState.IsValid)
            {
                var gameExtendGameDto = _mapper.Map<ExtendGameDTO>(game);

                if (image != null)
                {
                    var pictureName = gameExtendGameDto.Key + "_" + image.FileName;
                    image.SaveAs(Server.MapPath($"~/Content/Images/Games/{pictureName}"));
                    gameExtendGameDto.ImageName = pictureName;
                    gameExtendGameDto.ImageMimeType = image.ContentType;
                }
                else
                {
                    gameExtendGameDto.ImageName = "Game-Zone.png";
                    gameExtendGameDto.ImageMimeType = "image/png";
                }

                _gameService.Update(gameExtendGameDto);

                return RedirectToAction("FilteredGames");
            }

            return View(GetGameViewModelForUpdate(game));
        }

        [HttpPost]
        public ActionResult SetImage(string gamekey, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var pictureName = image.FileName;
                var imageMimeType = image.ContentType;
                image.SaveAs(Server.MapPath($"~/Content/Images/Games/{pictureName}"));
                _gameService.UpdateImage(gamekey, pictureName, imageMimeType);
            }

            return RedirectToAction("GetGame", "Game", new { gamekey });
        }

        [HttpPost]
        public async Task<ActionResult> SetImageAsync(string gamekey, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var pictureName = image.FileName;
                var imageMimeType = image.ContentType;
                _gameService.UpdateImage(gamekey, pictureName, imageMimeType);
                await Task.Run(() => image.SaveAs(Server.MapPath($"~/Content/Images/Games/{pictureName}")));
            }

            return RedirectToAction("GetGame", "Game", new { gamekey });
        }

        [HttpPost]
        public async Task<ActionResult> PostPicture(string gamekey, HttpPostedFileBase image)
        {
            HttpContext context = HttpContext.ApplicationInstance.Context;
            CustomHttpHandler customHttpHandler = new CustomHttpHandler();
            await customHttpHandler.ProcessRequestAsync(context);

            return RedirectToAction("GetGame", "Game", new { gamekey });
        }

            public ActionResult GetImage(string gamekey)
        {
            var game = _gameService.GetByKey(gamekey);
            var imagePath = Server.MapPath($"~/Content/Images/Games/{game.ImageName}");

            return game.ImageMimeType != null ? File(imagePath, game.ImageMimeType)
                : File("~/Content/Images/Game-Zone.png", "image/png");
        }

        public async Task<ActionResult> AsyncGetImage(string gamekey)
        {
            var game = await Task.Run(() => _gameService.GetByKey(gamekey));
            var imagePath = Server.MapPath($"~/Content/Images/Games/{game.ImageName}");

            return game.ImageMimeType != null ? File(imagePath, game.ImageMimeType)
                : File("~/Content/Images/Game-Zone.png", "image/png");
        }

        [HttpGet]
        public ActionResult GamesFilters(FilterViewModel filterViewModel)
        {
            var model = _filterViewModelBuilder.Rebuild(filterViewModel);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult FilteredGames(FilterViewModel filter, int page = 1)
        {
            if (filter.MinPrice > filter.MaxPrice)
            {
                ModelState.AddModelError("MinPrice", GlobalRes.MinMaxPrice);
            }

            if (filter.PageSize == 0)
            {
                filter.PageSize = PageSize.Ten;
            }

            var filterDto = _mapper.Map<FilterDTO>(filter);
            var filteredGames = _gameService.GetGamesByFilter(filterDto, page, filter.PageSize, out var itemsByFilter);

            var totalItem = 0;

            if (filter.PageSize == PageSize.All)
            {
                totalItem = _gameService.GetCountGame();
            }
            else
            {
                totalItem = (int)filter.PageSize;
            }

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = totalItem,
                TotalItemsByFilter = itemsByFilter
            };

            filter.PagingInfo = pagingInfo;

            filter = _filterViewModelBuilder.Rebuild(filter);

            if (filter.PageSize == 0)
            {
                filter.PageSize = PageSize.Ten;
            }

            var gameViewModel = _mapper.Map<IEnumerable<DetailsGameViewModel>>(filteredGames);

            if (gameViewModel.Any())
            {
                filter.Games = gameViewModel;
            }
            else
            {
                filter.Games = new List<DetailsGameViewModel>() { new DetailsGameViewModel() };
            }

            return View(filter);
        }

        [HttpGet]
        public ActionResult GetGame(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<DetailsGameViewModel>(gameDTO);

            return View(gameForView);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult GetAllDeleteGames()
        {
            var gamesDTO = _gameService.GetDeleteGames();
            var gamesForView = _mapper.Map<IEnumerable<DetailsGameViewModel>>(gamesDTO);

            return View(gamesForView);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Renew(string gamekey)
        {
            _gameService.Renew(gamekey);

            return RedirectToAction("FilteredGames");
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Remove(Guid gameId)
        {
            _gameService.Delete(gameId);

            return PartialView("GameDeleted");
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult Download(string gamekey)
        {
            var fileBytes = Encoding.ASCII.GetBytes(gamekey);

            return File(fileBytes, "text/plain", "test.txt");
        }

        [OutputCache(Duration = 60)]
        public ActionResult CountGames()
        {
            var gameCount = _gameService.GetCountGame();

            return PartialView("CountGames", gameCount);
        }

        [HttpGet]
        public ActionResult GetGameByPublisher()
        {
            var gameDTO = _gameService.GetGamesByPublisherId(CurrentUser.Id);

            if (gameDTO != null)
            {
                var gameForView = _mapper.Map<IEnumerable<DetailsGameViewModel>>(gameDTO);

                return View(gameForView);
            }

            return View("NothingToDisplay");
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
            genrelist.Select(genre =>
            {
                listGenreBoxes.Add(new CheckBox() { Text = genre.Name }); return genre;
            }).ToList();
            gameViewModel.ListGenres = listGenreBoxes;

            var listPlatformBoxes = new List<CheckBox>();
            platformlist.Select(platform =>
            {
                listPlatformBoxes.Add(new CheckBox() { Text = platform.Name }); return platform;
            }).ToList();
            gameViewModel.ListPlatformTypes = listPlatformBoxes;

            return gameViewModel;
        }

        private GameViewModel GetGameViewModelForCreate(GameViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForGameViewModel(gameViewModel);

            if (gameViewModel.SelectedPlatformTypesName != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes
                    .Where(x => gameViewModel.SelectedPlatformTypesName.Contains(x.Text));
            }

            if (gameViewModel.SelectedGenresName != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres
                    .Where(x => gameViewModel.SelectedGenresName.Contains(x.Text));
            }

            return gameViewModel;
        }

        private GameViewModel GetGameViewModelForUpdate(GameViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForGameViewModel(gameViewModel);
            gameViewModel = GetGameViewModelForCreate(gameViewModel);

            if (gameViewModel.PlatformTypes != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes
                    .Where(x => gameViewModel.PlatformTypes.Any(g => g.Name.Contains(x.Text)));
            }

            if (gameViewModel.Genres != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres
                    .Where(x => gameViewModel.Genres.Any(g => g.Name.Contains(x.Text)));
            }

            return gameViewModel;
        }
    }
}