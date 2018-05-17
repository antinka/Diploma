﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;

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
            return View(GetGameViewModel(new GameViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                if (game.SelectedGenresName == null)
                {
                    ModelState.AddModelError("Genres", "Please choose one or more genres");
                }

                if (game.SelectedPlatformTypesName == null)
                {
                    ModelState.AddModelError("PlatformTypes", "Please choose one or more platform types");
                }

                if (ModelState.IsValid)
                {
                    if (_gameService.AddNew(_mapper.Map<GameDTO>(game)))
                        return RedirectToAction("GetAllGames");

                    ModelState.AddModelError("Key", "Game with such key already exist, please enter another name");
                }
            }

            return View(GetGameViewModel(game));
        }

        [HttpGet]
        public ActionResult Update(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<GameViewModel>(gameDTO);

            gameForView = GetGameViewModel(gameForView);

            return View(gameForView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                if (game.SelectedGenresName == null)
                {
                    ModelState.AddModelError("Genres", "Please choose one or more genres");
                }

                if (game.SelectedPlatformTypesName == null)
                {
                    ModelState.AddModelError("PlatformTypes", "Please choose one or more platform types");
                }

                if (ModelState.IsValid)
                {
                    if (_gameService.Update(_mapper.Map<GameDTO>(game)))
                        return RedirectToAction("GetAllGames");

                    ModelState.AddModelError("Key", "Game with such key already exist, please enter another name");
                }
            }

            return View(GetGameViewModel(game));
        }

        [HttpGet]
        public ActionResult GetGame(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<GameViewModel>(gameDTO);

            return View(gameForView);
        }

        [HttpGet]
        public ActionResult GetAllGames()
        {
            var gamesDTO = _gameService.GetAll();
            var gamesForView = _mapper.Map<IEnumerable<GameViewModel>>(gamesDTO);

            return View(gamesForView);
        }

        [HttpPost]
        public ActionResult Remove(Guid gameId)
        {
            _gameService.Delete(gameId);

            return RedirectToAction("GetAllGames");
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

        private GameViewModel GetGameViewModel(GameViewModel gameViewModel)
        {
            var genrelist = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            var platformlist = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());

            gameViewModel.PublisherList = new SelectList(publishers, "Id", "Name");

			//todo Boxs - rename pls
            var listGenreBoxs = new List<CheckBox>();
			//todo .ForEach works slower than default foreach(). U could use .Select() extension in this case
            genrelist.ToList().ForEach(genre => listGenreBoxs.Add(new CheckBox() { Text = genre.Name }));
            gameViewModel.ListGenres = listGenreBoxs;

			//todo same, Boxs and Select();
            var listPlatformBoxs = new List<CheckBox>();
            platformlist.ToList().ForEach(platform => listPlatformBoxs.Add(new CheckBox() { Text = platform.Name }));
            gameViewModel.ListPlatformTypes = listPlatformBoxs;

			//todo why u need all this checks?
            if (gameViewModel.SelectedGenresName != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres.Where(x => gameViewModel.SelectedGenresName.Contains(x.Text));
            }
			//todo why u need all this checks?
			if (gameViewModel.Genres != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres.Where(x => gameViewModel.Genres.Any(g => g.Name.Contains(x.Text)));
            }
			//todo why u need all this checks?
			if (gameViewModel.SelectedPlatformTypesName != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes.Where(x => gameViewModel.SelectedPlatformTypesName.Contains(x.Text));
            }
			//todo why u need all this checks?
			if (gameViewModel.PlatformTypes != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes.Where(x => gameViewModel.PlatformTypes.Any(g => g.Name.Contains(x.Text)));
            }

            return gameViewModel;
        }
    }
}