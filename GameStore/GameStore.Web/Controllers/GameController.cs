﻿using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.Controllers
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
            return View(GetGameViewModelForCreate(new GameViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                var gameDTO = _mapper.Map<GameDTO>(game);

                if (game.SelectedGenresName == null)
                {
                    ModelState.AddModelError("Genres", "Please choose one or more genres");
                }

                if (game.SelectedPlatformTypesName == null)
                {
                    ModelState.AddModelError("SelectedPlatformTypesName", "Please choose one or more platform types");
                }

                if (!_gameService.IsUniqueKey(gameDTO))
                {
                    ModelState.AddModelError("Key", "Game with such key already exist, please enter another name");
                }

                if (ModelState.IsValid)
                {
                    _gameService.AddNew(gameDTO);

                    return RedirectToAction("FilteredGames");
                }
            }

            return View(GetGameViewModelForCreate(game));
        }

        [HttpGet]
        public ActionResult Update(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<GameViewModel>(gameDTO);

            gameForView = GetGameViewModelForUpdate(gameForView);

            return View(gameForView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                var gameDTO = _mapper.Map<GameDTO>(game);

                if (game.SelectedGenresName == null)
                {
                    ModelState.AddModelError("Genres", "Please choose one or more genres");
                }

                if (game.SelectedPlatformTypesName == null)
                {
                    ModelState.AddModelError("PlatformTypes", "Please choose one or more platform types");
                }

                if (!_gameService.IsUniqueKey(gameDTO))
                {
                    ModelState.AddModelError("Key", "Game with such key already exist, please enter another name");
                }

                if (ModelState.IsValid)
                {
                    _gameService.Update(gameDTO);

                    return RedirectToAction("FilteredGames");
                }
            }

            return View(GetGameViewModelForUpdate(game));
        }

        [HttpGet]
        public ActionResult GamesFilters(FilterViewModel filterViewModel)
        {
            var model = GetFilterViewModel(filterViewModel);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult FilteredGames(FilterViewModel filterViewModel, int page = 1)
        {
            var gamesByFilter = _gameService.GetGamesByFilter(_mapper.Map<FilterDTO>(filterViewModel), page, filterViewModel.PageSize);
            filterViewModel = GetFilterViewModel(filterViewModel);
            int totalItem;

            if (filterViewModel.PageSize == 0)
            {
                totalItem = _gameService.GetAll().Count();
            }
            else
            {
                totalItem = (int) filterViewModel.PageSize;
            }

            var gameViewModel = _mapper.Map<IEnumerable<GameViewModel>>(gamesByFilter);

            if (gameViewModel.Any())
            {
                filterViewModel.Games = gameViewModel;
            }
            else
            {
                filterViewModel.Games = new List<GameViewModel>() { new GameViewModel() };
            }

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = totalItem,
                TotalItems = _gameService.GetGamesByFilter(_mapper.Map<FilterDTO>(filterViewModel)).Count()
            };

            filterViewModel.PagingInfo = pagingInfo;

            if (filterViewModel.PagingInfo.TotalItems != 0)
            {
                return View(filterViewModel);
            }

            return View(filterViewModel);
        }

        [HttpGet]
        public ActionResult GetGame(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<DetailsGameViewModel>(gameDTO);

            _gameService.IncreaseGameView(gameDTO.Id);

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
        public ActionResult Download(Guid gamekey)
        {
            var path = Server.MapPath("~/Files/test.pdf");
            var mas = System.IO.File.ReadAllBytes(path);

            return File(mas, "application/pdf");
        }

      
        [OutputCache(Duration = 60)]
        public ActionResult CountGames()
        {
            var gameCount = _gameService.GetCountGame();

            return PartialView("CountGames", gameCount);
        }

        private GameViewModel CreateCheckBoxForGameViewModel(GameViewModel gameViewModel)
        {
            var genrelist = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            var platformlist = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());

            gameViewModel.PublisherList = new SelectList(publishers, "Id", "Name");

            var listGenreBoxes = new List<CheckBox>();
            genrelist.Select(genre => {listGenreBoxes.Add(new CheckBox() {Text = genre.Name}); return genre; }).ToList();
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
            publisherlist.ToList().ForEach(publisher => listPublisherBoxs.Add(new CheckBox() { Text = publisher.Name }));
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

            if (filterViewMode.MaxPrice != null)
            {
                model.MaxPrice = filterViewMode.MaxPrice;
            }

            if (filterViewMode.MinPrice != null)
            {
                model.MinPrice = filterViewMode.MinPrice;
            }

            if (filterViewMode.SearchGameName != null)
            {
                model.SearchGameName = filterViewMode.SearchGameName;
            }

            return model;
        }
    }
}