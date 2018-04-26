using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Filters;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            var platformTypes = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());

            var game = new GameViewModel
            {
                GenreList = new SelectList(genres, "Id", "Name"),
                PlatformTypeList = new SelectList(platformTypes, "Id", "Name"),
                PublisherList = new SelectList(publishers, "Id", "Name")
            };

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

            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            var platformTypes = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());

            game.GenreList = new SelectList(genres, "Id", "Name");
            game.PlatformTypeList = new SelectList(platformTypes, "Id", "Name");
            game.PublisherList = new SelectList(publishers, "Id", "Name");

            return View(game);
        }

        [HttpGet]
        public ActionResult Update(string gamekey)
        {
            var gameDTO = _gameService.GetByKey(gamekey);
            var gameForView = _mapper.Map<GameViewModel>(gameDTO);

            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());


            gameForView = GetGameViewModel(gameForView);
            gameForView.PublisherList = new SelectList(publishers, "Id", "Name");

            return View(gameForView);
        }

        private GameViewModel GetGameViewModel(GameViewModel gameViewModel)
        {
            var genrelist = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            var platformlist = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());

            var listGenreBoxs = new List<CheckBox>();
            genrelist.ToList().ForEach(genre => listGenreBoxs.Add(new CheckBox() { Text = genre.Name }));
            gameViewModel.ListGenres = listGenreBoxs;

            var listPlatformBoxs = new List<CheckBox>();
            platformlist.ToList().ForEach(platform => listPlatformBoxs.Add(new CheckBox() { Text = platform.Name }));
            gameViewModel.ListPlatformTypes = listPlatformBoxs;

            if (gameViewModel.Genres != null)
            {
                gameViewModel.SelectedGenres = gameViewModel.ListGenres.Where(x => gameViewModel.Genres.Any(g => g.Name.Contains(x.Text)));
            }
            if (gameViewModel.PlatformTypes != null)
            {
                gameViewModel.SelectedPlatformTypes = gameViewModel.ListPlatformTypes.Where(x => gameViewModel.PlatformTypes.Any(g => g.Name.Contains(x.Text)));
            }

            return gameViewModel;
        }

        [HttpPost]
        public ActionResult Update(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                _gameService.Update(_mapper.Map<GameDTO>(game));

                return RedirectToAction("GetAllGames");
            }

            return View(game);
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

        [OutputCache(Duration = 60)]
        public ActionResult Remove(Guid gameId)
        {
            _gameService.Delete(gameId);

            return RedirectToAction("GetAllGames");
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
            var gameCount = _gameService.GetCountGame();

            return PartialView("CountGames", gameCount);
        }
    }
}