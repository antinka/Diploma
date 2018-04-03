using AutoMapper;
using GameStore.BLL.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        private IMapper _mapper;

        public HomeController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper=mapper;
        }

        public ActionResult Index()
        {
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

    }
}