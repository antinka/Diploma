using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Filters;
using GameStore.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    [TrackRequestIP]
    [ExceptionFilter]
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        ILog log = LogManager.GetLogger("LOGGER");

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }
       
        [OutputCache(Duration = 60)]
        [HttpPost]
        public JsonResult New(GameViewModel game)
        {
            gameService.AddNewGame(AutoMapper.Mapper.Map<GameViewModel,GameDTO>(game));
            return Json("Add new game", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Update(GameViewModel game)
        {
            gameService.EditGame(AutoMapper.Mapper.Map<GameViewModel, GameDTO>(game));
            return Json("Add new game", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult GetGameById(Guid? key)
        {
            GameDTO game = gameService.GetGame(key.GetValueOrDefault());
            return Json("GetGameById", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult GetAllGames()
        {
            IEnumerable<GameDTO> games = gameService.GetAllGame();
            return Json("GetAllGames", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult Remove(Guid key)
        {
            gameService.DeleteGame(key);
            return Json("Remove Games", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpGet]
        public FileResult Download(Guid gamekey)
        {
            string path = Server.MapPath("~/Files/test.txt");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/txt";
            string file_name = "test.txt";
            return File(mas, file_type, file_name);
        }
    }
}