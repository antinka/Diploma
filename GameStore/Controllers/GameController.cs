using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Filters;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.BLL.Enums;

namespace GameStore.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, 
            IGenreService genreService, 
            IPlatformTypeService platformTypeService,
            IMapper mapper,
            IPublisherService publisherService)
        {
            _gameService = gameService;
            _mapper = mapper;
            _publisherService = publisherService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
        }

        [HttpGet]
        public ActionResult New()
        {
            GameViewModel game = new GameViewModel();
            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            game.GenreList = new SelectList(genres, "Id", "Name");
            var platformTypes = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            game.PlatformTypeList = new SelectList(platformTypes, "Id", "Name");
            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());
            game.PublisherList = new SelectList(publishers, "Id", "Name");

            return View(game);
        }

        [HttpPost]
        public ActionResult New(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                _gameService.AddNew(_mapper.Map<GameDTO>(game));

                return RedirectToAction("GetAllGames");
            }
            else
            {
                var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
                game.GenreList = new SelectList(genres, "Id", "Name");
                var platformTypes = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
                game.PlatformTypeList = new SelectList(platformTypes, "Id", "Name");
                var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());
                game.PublisherList = new SelectList(publishers, "Id", "Name");

                return View(game);
            }
        }

        [HttpPost]
        public ActionResult Update(GameViewModel game)
        {
            _gameService.Update(_mapper.Map<GameDTO>(game));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        

        [HttpGet]
        public ActionResult Games(FilterViewModel filterViewModel)
        {
            var page = (filterViewModel.Page >= 1) ? filterViewModel.Page : 1;
            filterViewModel.Page = page;
            filterViewModel.PageSize = PageSize.All;

            var gamesByFilter = _gameService.GetGamesByFilter(_mapper.Map<FilterDTO>(filterViewModel), filterViewModel.Page, filterViewModel.PageSize);
            filterViewModel.TotalItems = gamesByFilter.Count();

            return View(filterViewModel);
        }

        [HttpGet]
        public ActionResult GamesFilters(FilterViewModel filterViewModel)
        {
            var model = GetFilterViewModel(filterViewModel);
          
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult FilteredGames(FilterViewModel filterViewModel)
        {
            var gamesByFilter = _gameService.GetGamesByFilter(_mapper.Map<FilterDTO>(filterViewModel), filterViewModel.Page, filterViewModel.PageSize);
            var gameViewModel = _mapper.Map<IEnumerable<GameViewModel>>(gamesByFilter);

            var gamesListViewModel = new GamesListViewModel()
            {
                Games = gameViewModel,
                ItemsPerPage = filterViewModel.PageSize,
                Page = filterViewModel.Page,
                TotalItems = gamesByFilter.Count()
            };

            return PartialView(gamesListViewModel); ;
        }

        private FilterViewModel GetInitFilterViewModel()
        {
            var model = new FilterViewModel();

            var genrelist = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            var platformlist = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publisherlist = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());

            var listGenreBoxs = new List<CheckBox>();
            genrelist.ToList().ForEach(genre => listGenreBoxs.Add(new CheckBox() { Text = genre.Name }));
            model.ListGenres = listGenreBoxs;
            var listPlatformBoxs = new List<CheckBox>();
            platformlist.ToList().ForEach(platform => listPlatformBoxs.Add(new CheckBox() { Text = platform.Name }));
            model.ListPlatformTypes = listPlatformBoxs;
            var listPublisherBoxs = new List<CheckBox>();
            publisherlist.ToList().ForEach(publisher => listPublisherBoxs.Add(new CheckBox() { Text = publisher.Name}));
            model.ListPublishers = listPublisherBoxs;

            return model;
        }

        private FilterViewModel GetFilterViewModel(FilterViewModel filterViewMode)
        {
            var model = GetInitFilterViewModel();

            if (filterViewMode.SelectedGenresName != null)
            {
                model.SelectedGenres = model.ListGenres.Where(x => filterViewMode.SelectedGenresName.Contains(x.Text));
                model.SelectedGenresName = filterViewMode.SelectedGenresName;
            }

            if (filterViewMode.SelectedPlatformTypesName != null)
            {
                model.SelectedPlatformTypes = model.ListPlatformTypes.Where(x => filterViewMode.SelectedPlatformTypesName.Contains(x.Text));
                model.SelectedPlatformTypesName = filterViewMode.SelectedPlatformTypesName;
            }

            if (filterViewMode.SelectedPublishersName != null)
            {
                model.SelectedPublishers = model.ListPublishers.Where(x => filterViewMode.SelectedPublishersName.Contains(x.Text));
                model.SelectedPublishersName = filterViewMode.SelectedPublishersName;
            }

            return model;
        }

        [HttpGet]
        public ActionResult GetGame(string gamekey)
        {
            var game = _gameService.GetByKey(gamekey);

            return View(_mapper.Map<GameViewModel>(game));
        }

        [HttpGet]
        public ActionResult GetAllGames()
        {
            var games = _gameService.GetAll();

            return View((_mapper.Map <IEnumerable<GameViewModel>>(games)));
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Remove(Guid gameId)
        {
            _gameService.Delete(gameId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public FileResult Download(Guid gamekey)
        {
            var path = Server.MapPath("~/Files/test.txt");
            var mas = System.IO.File.ReadAllBytes(path);
            var fileType = "application/txt";
            var fileName = "test.txt";

            return File(mas, fileType, fileName);
        }

        public ActionResult CountGames()
        {
            var gameCount = _gameService.GetAll().Count();

            return PartialView("CountGames", gameCount);
        }
    }
}