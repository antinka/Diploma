using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.Payments;
using GameStore.Web.Payments.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            _stratagy = new List<IPayment> {_bankMock, _boxMock, _visaMock};

            _paymentStrategy = new PaymentStrategy(_stratagy);
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _ordersService = new Mock<IOrdersService>();
            _gameService = new Mock<IGameService>();
            _sut = new OrderController(_ordersService.Object, _gameService.Object, _mapper, _paymentStrategy);
        }

        [Fact]
        public void BasketInfo_Verifiable()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Verifiable();
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            _sut.BasketInfo();

            _ordersService.Verify(s => s.GetOrder(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void AddGameToOrder_GameKeyWhereGameUnitsInStockMorethen1_Verifiable()
        {
            var fakeGameKey = "fakeGameKey";
            var fakeUserId = Guid.NewGuid();
            var fakeGameId = Guid.NewGuid();
            var fakeGame = new GameDTO()
            {
                Id = Guid.NewGuid(),
                Key = fakeGameKey,
                UnitsInStock = 5
            };

            _gameService.Setup((service => service.GetByKey(fakeGameKey))).Returns(fakeGame);
            _ordersService.Setup(service => service.AddNewOrderDetails(fakeUserId, fakeGameId)).Verifiable();
            var httpRequest = new HttpRequest("", "http://mySomething", "");
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
            var fakeGame = new GameDTO()
            {
                Id = Guid.NewGuid(),
                Key = fakeGameKey,
                UnitsInStock = 0
            };

            _gameService.Setup((service => service.GetByKey(fakeGameKey))).Returns(fakeGame);
            var httpRequest = new HttpRequest("", "http://mySomething", "");
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
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            _sut.DeleteGameFromOrder(fakeGameId);

            _ordersService.Verify(s => s.DeleteGameFromOrder(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Pay_PaymentTypesBank_FileStreamResult()
        {
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrder(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Bank);

            Assert.Equal(typeof(FileStreamResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesBox_ReturnViewResult()
        {
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrder(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Box);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesVisa_ReturnViewResult()
        {
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrder(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Visa);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void CountGamesInOrder_ReturnPartialViewResult()
        {
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);

            var res = _sut.CountGamesInOrder();

            Assert.Equal(typeof(PartialViewResult), res.GetType());
        }

        [Fact]
        public void Order_ReturnViewResult()
        {
            var httpRequest = new HttpRequest("", "http://mySomething", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
            _ordersService.Setup(service => service.GetOrder(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Order();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
