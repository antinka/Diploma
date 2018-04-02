using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Filters;
using GameStore.Models;
using System;
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
        public JsonResult New(GameViewModel game)
        {
            _gameService.AddNewGame(_mapper.Map<GameViewModel,GameDTO>(game));
            return Json("Add new game", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Update(GameViewModel game)
        {
            _gameService.UpdateGame(_mapper.Map<GameViewModel, GameDTO>(game));
            return Json("Add new game", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult GetGameById(Guid? key)
        {
            GameDTO game = _gameService.GetGame(key.GetValueOrDefault());
            return Json("GetGameById", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult GetAllGames()
        {
            var games = _gameService.GetAllGame();
            return Json("GetAllGames", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Remove(Guid key)
        {
            _gameService.DeleteGame(key);
            return Json("Remove Games", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public FileResult Download(Guid gamekey)
        {
            var path = Server.MapPath("~/Files/test.txt");
            var mas = System.IO.File.ReadAllBytes(path);
            var file_type = "application/txt";
            var file_name = "test.txt";
            return File(mas, file_type, file_name);
        }
    }
}