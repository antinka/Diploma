using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Filters;
using GameStore.ViewModels;
using System;
using System.Net;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }
       
        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult New(GameViewModel game)
        {
            _gameService.AddNew(_mapper.Map<GameDTO>(game));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Update(GameViewModel game)
        {
            _gameService.Update(_mapper.Map<GameDTO>(game));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult GetGame(Guid gamekey)
        {
            var game = _gameService.Get(gamekey);

            return Json(game, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult GetAllGames()
        {
            var games = _gameService.GetAll();

            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Remove(Guid gamekey)
        {
            _gameService.Delete(gamekey);

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
    }
}