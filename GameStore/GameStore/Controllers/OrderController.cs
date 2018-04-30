using AutoMapper;
using GameStore.BLL.Exeption;
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
        private readonly IMapper _mapper;

        private IPayment payment { get; set; }

        public OrderController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        public ActionResult BasketInfo()
        {
            var userId = Guid.Empty;

            try
            {
                var order = _ordersService.GetOrder(userId);
                var orderViewModel = _mapper.Map<OrderViewModel>(order);

                return View(orderViewModel);
            }
            catch (EntityNotFound)
            {
                return View("EmptyBasket");
            }
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

                return RedirectToAction("BasketInfo");
            }

            return View(basket);
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