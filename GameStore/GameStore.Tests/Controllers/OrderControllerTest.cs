﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.Payments;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;
using Moq;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrdersService> _ordersService;
        private readonly Mock<IGameService> _gameService;
        private readonly IPaymentStrategy _paymentStrategy;
        private readonly IMapper _mapper;
        private readonly OrderController _sut;
        private Bank _bankMock;
        private Box _boxMock;
        private Visa _visaMock;
        private List<IPayment> _stratagy;

        public OrderControllerTest()
        {
            _bankMock = new Bank();
            _boxMock = new Box();
            _visaMock = new Visa();
            _stratagy = new List<IPayment> { _bankMock, _boxMock, _visaMock };

            _paymentStrategy = new PaymentStrategy(_stratagy);
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _ordersService = new Mock<IOrdersService>();
            _gameService = new Mock<IGameService>();
            _sut = new OrderController(_ordersService.Object, _gameService.Object, _mapper, _paymentStrategy, null);
        }

        [Fact]
        public void BasketInfo_Verifiable()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrderByUserId(fakeUserId)).Verifiable();
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            var fakeOrder = new OrderDTO() { Id = Guid.NewGuid(), UserId = fakeUserId };

            _ordersService.Setup(service => service.GetOrderByUserId(fakeUserId)).Returns(fakeOrder);
            _ordersService.Setup(service => service.GetAllShippers()).Returns(new List<ShipperDTO>());

            var res = _sut.BasketInfo();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void AddGameToOrder_GameKeyWhereGameUnitsInStockMorethen1_Verifiable()
        {
            var fakeGameKey = "fakeGameKey";
            var fakeUserId = Guid.NewGuid();
            var fakeGameId = Guid.NewGuid();
            var fakeGame = new ExtendGameDTO()
            {
                Id = Guid.NewGuid(),
                Key = fakeGameKey,
                UnitsInStock = 5
            };

            _gameService.Setup(service => service.GetByKey(fakeGameKey)).Returns(fakeGame);
            _ordersService.Setup(service => service.AddNewOrderDetails(fakeUserId, fakeGameId)).Verifiable();
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            _sut.AddGameToOrder(fakeGameKey);

            _ordersService.Verify(s => s.AddNewOrderDetails(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void AddGameToOrder_GameKeyWhereGameUnitsInStockLessThen1_ReturnedViewResult()
        {
            var fakeGameKey = "fakeGameKey";
            var fakeGame = new ExtendGameDTO()
            {
                Id = Guid.NewGuid(),
                Key = fakeGameKey,
                UnitsInStock = 0
            };

            _gameService.Setup(service => service.GetByKey(fakeGameKey)).Returns(fakeGame);
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            var res = _sut.AddGameToOrder(fakeGameKey);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void DeleteGameFromOrder_gameId_Verifiable()
        {
            var fakeUserId = Guid.NewGuid();
            var fakeGameId = Guid.NewGuid();

            _ordersService.Setup(service => service.DeleteGameFromOrder(fakeUserId, fakeGameId)).Verifiable();
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            _sut.DeleteGameFromOrder(fakeGameId);

            _ordersService.Verify(s => s.DeleteGameFromOrder(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Pay_PaymentTypesBank_FileStreamResult()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Bank);

            Assert.Equal(typeof(FileStreamResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesBox_ReturnViewResult()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Box);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesVisa_ReturnViewResult()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Visa);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void CountGamesInOrder_ReturnPartialViewResult()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            var res = _sut.CountGamesInOrder();

            Assert.Equal(typeof(PartialViewResult), res.GetType());
        }

        [Fact]
        public void Order_ReturnViewResult()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Order();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void UpdateShipper_OrderViewModel_ReturnedRedirectToRouteResult()
        {
            var fakeOrderViewModel = new OrderViewModel() { Id = Guid.NewGuid() };

            var res = _sut.UpdateShipper(fakeOrderViewModel) as RedirectToRouteResult;

            Assert.Equal("BasketInfo", res.RouteValues["action"]);
        }

        [Fact]
        public void FilterOrders_FilterOrder_ReturnedViewResult()
        {
            var fakeFilterOrder = new FilterOrder();

            var res = _sut.HistoryOrders(fakeFilterOrder);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
