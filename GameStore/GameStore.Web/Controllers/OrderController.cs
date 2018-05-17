using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.Payments;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

		//todo use underscore for private instances
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

        public ActionResult AddGameToOrder(string gameKey)
        {
            var userId = Guid.Empty;

            var game = _gameService.GetByKey(gameKey);
			//todo if UnitsInStock would be 1?
            if (game.UnitsInStock > 1)
            {
                _ordersService.AddNewOrderDetails(userId, game.Id);

                var basket = new BasketViewModel()
                {
                    GameName = game.Name,
                    Price = game.Price
                };

                return View(basket);
            }

            return View("NotEnoughGameInStock");
        }

        public ActionResult DeleteGameFromOrder(Guid gameId)
        {
            var userId = Guid.Empty;

            _ordersService.DeleteGameFromOrder(userId, gameId);

             return RedirectToAction("BasketInfo");
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

			//todo move creationg this to PaymentStrategy. And inject it.
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

        public ActionResult Order()
        {
            var userId = Guid.Empty;
            var order = _ordersService.GetOrder(userId);
            var orderDetailsViewModel = _mapper.Map<IEnumerable<OrderDetailViewModel>>(order.OrderDetails);

            return View(orderDetailsViewModel);
        }
    }
}