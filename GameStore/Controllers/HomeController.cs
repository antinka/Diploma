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
            // return gameService.GetGamesByGenre(Guid.Parse("E73C7A6C-6D9A-4606-B9FA-5F617C847125")).Count.ToString();
            return gameService.GetGamesByPlatformType(Guid.Parse("8a36e672-683a-48c3-8ccf-5983549448e7")).Count().ToString();
        }

    }
}