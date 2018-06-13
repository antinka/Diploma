using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class ShipperController : BaseController
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public ShipperController(
            IOrdersService ordersService,
            IMapper mapper,
            IAuthentication authentication) : base(authentication)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        public ActionResult GetAll()
        {
            var shippers = _mapper.Map<IEnumerable<ShipperViewModel>>(_ordersService.GetAllShippers());

            return View(shippers);
        }
    }
}