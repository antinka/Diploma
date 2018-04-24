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
			//todo refactor please, unreadable
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
			//todo else
            else
            {
				//todo unreadable
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