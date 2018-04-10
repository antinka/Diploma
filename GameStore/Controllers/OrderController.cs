using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Payments.Enums;
using GameStore.ViewModels;
using System;
using System.Web.Mvc;
using GameStore.Payments;

namespace GameStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;
        
        public OrderController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        public ActionResult BasketInfo()
        {
            var userId = Guid.Empty;
            var orders = _mapper.Map<OrderViewModel>(_ordersService.GetOrderDetail(userId));

            return View(orders);
        }
        
        [HttpGet]
        public ActionResult AddGameToOrder(Guid gameId, short unitsInStock)
        {
            var userId = Guid.Empty;

            var basket = new BasketViewModel()
            {
                UserId = userId,
                GameId = gameId,
                UnitsInStock = unitsInStock
            };

            return View(basket);
        }

        [HttpPost]
        public ActionResult AddGameToOrder(BasketViewModel basket)
        {
            if (ModelState.IsValid)
            {
                _ordersService.AddNewOrderDetails(basket.UserId, basket.GameId, basket.Quantity);

                return View("BasketInfo");
            }

            return View(basket);
        }

        [HttpGet]

        public ActionResult Pay(PaymentTypes payment)
        {
            var userId = Guid.Empty;
            var order = Mapper.Map<OrderViewModel>(_ordersService.GetOrderDetail(userId));

            Payment pay = new Payment(payment);

            //_service.Pay(username);

            return pay.Pay(order, View);
           // return View();
        }
    }
}