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
        private readonly IGameService gameService;
        ILog log = LogManager.GetLogger("LOGGER");
        IMapper mapper = MapperConfigUI.GetMapper();

        public HomeController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public string Index()
        {
            // return gameService.GetGamesByGenre(Guid.Parse("608d7a2d-7b81-4edb-83f1-a92877e79ea8")).Count().ToString();
             return gameService.GetGamesByPlatformType(Guid.Parse("0a7f170f-1158-4c69-8fe1-5c87ae2743a6")).Count().ToString();
            // return "asd";
            // return gameService.GetAllGame().Count().ToString();
           // return gameService.GetGame(Guid.Parse("5f29f5db-9dfd-45dc-b739-57be4e8555d5")).Name.ToString();
        }

    }
}