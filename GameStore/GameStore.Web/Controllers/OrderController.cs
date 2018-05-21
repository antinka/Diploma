using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.Payments;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IPayment> _payments;

        public OrderController(IOrdersService ordersService, IGameService gameService, IMapper mapper, IEnumerable<IPayment> payments)
        {
            _ordersService = ordersService;
            _gameService = gameService;
            _mapper = mapper;
            _payments = payments;
        }

        [HttpGet]
        public ActionResult BasketInfo()
        {
            var userId = GetUserId();

            var order = _ordersService.GetOrder(userId);

            if (order == null || !order.OrderDetails.Any())
                return View("EmptyBasket");

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        [HttpGet]
        public ActionResult AddGameToOrder(string gameKey)
        {
            var userId = GetUserId();

            var game = _gameService.GetByKey(gameKey);

            if (game.UnitsInStock >= 1)
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

        [HttpGet]
        public ActionResult DeleteGameFromOrder(Guid gameId)
        {
            var userId = GetUserId();

            _ordersService.DeleteGameFromOrder(userId, gameId);

            return RedirectToAction("BasketInfo");
        }

        [HttpGet]
        public ActionResult Pay(PaymentTypes paymentType)
        {
            var userId = GetUserId();
            var order = _ordersService.GetOrder(userId);

            var orderPay = new OrderPayment()
            {
                Id = order.Id,
                UserId = order.UserId,
                Cost = order.Cost
            };

            return new PaymentStrategy(_payments).GetPaymentStrategy(paymentType, orderPay);
        }
        [HttpGet]
        public ActionResult Order()
        {
            var userId = GetUserId();
            var order = _ordersService.GetOrder(userId);
            var orderDetailsViewModel = _mapper.Map<IEnumerable<OrderDetailViewModel>>(order.OrderDetails);

            return View(orderDetailsViewModel);
        }

        public ActionResult CountGamesInOrder()
        {
            var userId = GetUserId();

            var gameCount = _ordersService.CountGamesInOrder(userId);

            return PartialView("CountGamesInOrder", gameCount);
        }

        private Guid GetUserId()
        {
            Guid userId;

            if (HttpContext.Request.Cookies["userId"] != null)
            {
                userId = Guid.Parse(HttpContext.Request.Cookies["userId"].Value);
            }
            else
            {
                HttpContext.Response.Cookies["userId"].Value = Guid.NewGuid().ToString();
                userId = Guid.Parse(HttpContext.Request.Cookies["userId"].Value);
            }

            return userId;
        }
    }
}