using AutoMapper;
using GameStore.BAL.Interfaces;
using GameStore.Infastracture;
using log4net;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        private ILog _log = LogManager.GetLogger("LOGGER");
        private IMapper _mapper = MapperConfigUi.GetMapper();

        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public string Index()
        {
            // return gameService.GetGamesByGenre(Guid.Parse("0b893805-9db7-430e-92dd-9a297febc79e")).Count().ToString();
            return _gameService.GetGamesByPlatformType(Guid.Parse("911a0833-2cde-4c72-a6e4-7c201491a2e5")).Count().ToString();
            // return "asd";
           //return gameService.GetAllGame().Count().ToString();
           // return gameService.GetGame(Guid.Parse("36346c52-3767-43e7-8bbf-2496984ae2ed")).Name.ToString();
        }

    }
}