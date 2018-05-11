using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Payments;
using GameStore.Payments.Enums;
using GameStore.ViewModels;
using System;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        private IPayment payment { get; set; }

        public OrderController(IOrdersService ordersService, IGameService gameService, IMapper mapper)
        {
            _ordersService = ordersService;
            _gameService = gameService;
            _mapper = mapper;
        }

        public ActionResult BasketInfo()
        {
            var userId = Guid.Empty;
            var order = _ordersService.GetOrder(userId);

            if (order == null)
                return View("EmptyBasket");

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        [HttpGet]
        public ActionResult AddGameToOrder(string gameKey)
        {
            var userId = Guid.Empty;

            var game = _gameService.GetByKey(gameKey);

            var basket = new BasketViewModel()
            {
                UserId = userId,
                GameId = game.Id,
                UnitsInStock = game.UnitsInStock,
                GameName = game.Name
            };

            return View(basket);
        }

        [HttpPost]
        public ActionResult AddGameToOrder(BasketViewModel basket)
        {
            if (ModelState.IsValid)
            {
                _ordersService.AddNewOrderDetails(basket.UserId, basket.GameId, basket.Quantity);

                return View("Added");
            }

            return View(basket);
        }

        public ActionResult CountGamesInOrder()
        {
            var userId = Guid.Empty;

            var gameCount = _ordersService.CountGamesInOrder(userId);

            return PartialView("CountGamesInOrder", gameCount);
        }

        [HttpGet]
        public ActionResult Pay(PaymentTypes paymentType)
        {
            var userId = Guid.Empty;
            var order = _ordersService.GetOrder(userId);

            var orderPay = new OrderPayment()
            {
                Id = order.Id,
                UserId = order.UserId,
                Cost = order.Cost
            };

            if (paymentType == PaymentTypes.Bank)
            {
                payment = new Bank();
            }
            else if (paymentType == PaymentTypes.Box)
            {
                payment = new Box();
            }
            else if (paymentType == PaymentTypes.Visa)
            {
                payment = new Visa();
            }

            return payment.Pay(orderPay);
        }
    }
}