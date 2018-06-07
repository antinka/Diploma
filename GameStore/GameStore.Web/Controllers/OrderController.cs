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
using GameStore.BLL.DTO;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class OrderController : BaseController
    {
        private readonly IOrdersService _ordersService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IPaymentStrategy _paymentStrategy;

        public OrderController(IOrdersService ordersService,
            IGameService gameService, 
            IMapper mapper,
            IPaymentStrategy paymentStrategy,
            IAuthentication authentication) 
            
            : base(authentication)
        {
            _ordersService = ordersService;
            _gameService = gameService;
            _mapper = mapper;
            _paymentStrategy = paymentStrategy;
        }

        [HttpGet]
        public ActionResult BasketInfo()
        {
            var userId = GetUserId();

            var order = _ordersService.GetOrderByUserId(userId);

            if (order == null || !order.OrderDetails.Any())
                return View("EmptyBasket");

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            var shippers = _mapper.Map<IEnumerable<ShipperViewModel>>(_ordersService.GetAllShippers());
            orderViewModel.ShipperList = new SelectList(shippers, "Id", "CompanyName");

            return View(orderViewModel);
        }

        [HttpGet]
        public ActionResult AddGameToOrder(string gameKey)
        {
            var userId = GetUserId();

            var gameDTO = _gameService.GetByKey(gameKey);
            var game = _mapper.Map<DetailsGameViewModel>(gameDTO);

            if (game.UnitsInStock >= 1 && game.IsDelete == false)
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
            var order = _ordersService.GetOrderByUserId(userId);

            var orderPay = new OrderPayment()
            {
                Id = order.Id,
                UserId = order.UserId,
                Cost = order.Cost
            };

            return _paymentStrategy.GetPaymentStrategy(paymentType, orderPay);
        }

        [HttpGet]
        public ActionResult Order()
        {
            var userId = GetUserId();
            var order = _ordersService.GetOrderByUserId(userId);
            var orderDetailsViewModel = _mapper.Map<IEnumerable<OrderDetailViewModel>>(order.OrderDetails);

            return View(orderDetailsViewModel);
        }

        public ActionResult HistoryOrders(FilterOrder filterOrder)
        {
            
            if (filterOrder.DateTimeFrom != null && filterOrder.DateTimeTo != null && filterOrder.DateTimeFrom > filterOrder.DateTimeTo)
            {
                ModelState.AddModelError("", "Date Time From could not be bigger than Date Time To, please choose another one");
            }

            if (filterOrder.DateTimeTo == null)
            {
                filterOrder.DateTimeTo = DateTime.UtcNow.AddDays(-30);
            }

            var ordersDTO = _ordersService.GetOrdersBetweenDates(filterOrder.DateTimeFrom, filterOrder.DateTimeTo);
            filterOrder.OrdersViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(ordersDTO);

            return View(filterOrder);
        }

        public ActionResult UpdateShipper(OrderViewModel orderViewModel)
        {
            var orderDTO = _mapper.Map<OrderDTO>(orderViewModel);
            _ordersService.UpdateShipper(orderDTO);

            return RedirectToAction("BasketInfo");
        }

        public ActionResult CountGamesInOrder()
        {
            var userId = GetUserId();

            var gameCount = _ordersService.CountGamesInOrder(userId);

            return PartialView("CountGamesInOrder", gameCount);
        }

        public ActionResult AllOrders()
        {
            return View();
        }

        public ActionResult Orders(FilterOrder filterOrder)
        {

            if (filterOrder.DateTimeFrom != null && filterOrder.DateTimeTo != null && filterOrder.DateTimeFrom > filterOrder.DateTimeTo)
            {
                ModelState.AddModelError("", "Date Time From could not be bigger than Date Time To, please choose another one");
            }

            if (filterOrder.DateTimeFrom == null)
            {
                filterOrder.DateTimeFrom = DateTime.UtcNow.AddDays(-30);
            }

            var ordersDTO = _ordersService.GetOrdersWithUnpaidBetweenDates(filterOrder.DateTimeFrom, filterOrder.DateTimeTo);
            filterOrder.OrdersViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(ordersDTO);

            return View(filterOrder);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult EditOrder(Guid userId)
        {
            var orderDto = _ordersService.GetOrderByUserId(userId);
            var orderViewModel = _mapper.Map<OrderViewModel>(orderDto);

            return View(orderViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult EditOrder(OrderViewModel orderView)
        {
            if (orderView.IsPaid == false && orderView.ShippedDate != null)
            {
                ModelState.AddModelError("ShippedDate", "order could not be delivered without paying");
            }

            if (orderView.IsPaid && orderView.Date == null)
            {
                orderView.Date = DateTime.Now.AddDays(10);
            }

            if (ModelState.IsValid)
            {
                _ordersService.UpdateOrder(_mapper.Map<OrderDTO>(orderView));

                return RedirectToAction("Orders");
            }


            return View(orderView);
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
                userId = Guid.NewGuid();
                HttpContext.Response.Cookies["userId"].Value = userId.ToString();
            }

            return userId;
        }
    }
}