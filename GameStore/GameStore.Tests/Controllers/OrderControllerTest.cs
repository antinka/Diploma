using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Authorization.Implementation;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.Payments;
using GameStore.Web.Payments.Enums;
using GameStore.Web.Payments.ViewModels;
using GameStore.Web.ViewModels;
using Moq;
using Rotativa;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrdersService> _ordersService;
        private readonly Mock<IGameService> _gameService;
        private readonly Mock<IAuthentication> _authentication;
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
            _authentication = new Mock<IAuthentication>();
            _sut = new OrderController(
                _ordersService.Object,
                _gameService.Object,
                _mapper,
                _paymentStrategy,
                _authentication.Object);
        }

        [Fact]
        public void BasketInfo_ReturnedViewResult()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrderByOrderId(fakeUserId));
            CurrentUser();
            var fakeOrder = new OrderDTO() { Id = Guid.NewGuid(), UserId = fakeUserId };

            _ordersService.Setup(service => service.GetOrderByOrderId(fakeUserId)).Returns(fakeOrder);
            _ordersService.Setup(service => service.GetAllShippers()).Returns(new List<ShipperDTO>());

            var res = _sut.BasketInfo();

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void AddGameToOrder_GameKeyWhereGameUnitsInStockMorethen1_AddGameToOrderCalled()
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
            _ordersService.Setup(service => service.AddNewOrderDetails(fakeUserId, fakeGameId));
            CurrentUser();

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
            CurrentUser();

            var res = _sut.AddGameToOrder(fakeGameKey);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void DeleteGameFromOrder_gameId_DeleteGameFromOrderCalled()
        {
            var fakeUserId = Guid.NewGuid();
            var fakeGameId = Guid.NewGuid();
            _ordersService.Setup(service => service.DeleteGameFromOrder(fakeUserId, fakeGameId));
            CurrentUser();

            _sut.DeleteGameFromOrder(fakeGameId);

            _ordersService.Verify(s => s.DeleteGameFromOrder(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Pay_PaymentTypesBank_ReturnViewAsPdf()
        {
            CurrentUser();
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Bank);

            Assert.IsType<ViewAsPdf>(res);
        }

        [Fact]
        public void Pay_PaymentTypesBox_ReturnViewResult()
        {
            CurrentUser();
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Box);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Pay_PaymentTypesVisa_ReturnViewResult()
        {
            CurrentUser();
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Visa);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void CountGamesInOrder_ReturnPartialViewResult()
        {
            CurrentUser();

            var res = _sut.CountGamesInOrder();

            Assert.IsType<PartialViewResult>(res);
        }

        [Fact]
        public void Order_ReturnViewResult()
        {
            CurrentUser();
            _ordersService.Setup(service => service.GetOrderByUserId(It.IsAny<Guid>())).Returns(new OrderDTO());

            var res = _sut.Order();

            Assert.IsType<ViewResult>(res);
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

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Box_orderId_PayMethodCalled()
        {
            var fakeOrderId = Guid.NewGuid();
            _ordersService.Setup(service => service.Pay(It.IsAny<Guid>()));

            _sut.Box(fakeOrderId);

            _ordersService.Verify(s => s.Pay(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Visa_ValidvisaViewModel_PayMethodCalled()
        {
            var fakeOrderId = Guid.NewGuid();
            _ordersService.Setup(service => service.Pay(It.IsAny<Guid>()));

            _sut.Box(fakeOrderId);

            _ordersService.Verify(s => s.Pay(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Visa_InvalidvisaViewModel_ReturnViewResult()
        {
            var fakeVisaViewModel = new VisaViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Visa(fakeVisaViewModel);

            Assert.IsType<ViewResult>(res);
        }

        private void CurrentUser()
        {
            var userProvider = new UserProvider();
            _authentication.Setup(user => user.CurrentUser).Returns(userProvider);

            var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContextMock = new HttpContext(httpRequest, httpResponse);
            _sut.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), _sut);
        }
    }
}
