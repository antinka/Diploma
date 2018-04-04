using AutoMapper;
using GameStore.BLL.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public HomeController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        // GET: Home
        public string Index()
        {
             //return gameService.GetGamesByGenre(Guid.Parse("0b893805-9db7-430e-92dd-9a297febc79e")).Count().ToString();    
             return _gameService.GetGamesByPlatformType(Guid.Parse("66f0bedf-40b0-4109-a14f-707565137d7f")).Count().ToString();   
            // return "asd";              //  return _gameService.GetAllGame().Count().ToString();           
            
            // return _gameService.Get(Guid.Parse("36346c52-3767-43e7-8bbf-2496984ae2ed")).Name.ToString();
        }
    }
}