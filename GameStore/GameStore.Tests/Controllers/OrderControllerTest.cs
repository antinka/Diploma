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
        private readonly IMapper _mapper;
        private readonly OrderController _sut;


        public OrderControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _ordersService = new Mock<IOrdersService>();
            _gameService = new Mock<IGameService>();
            _sut = new OrderController(_ordersService.Object, _gameService.Object, _mapper , new List<IPayment>());
        }

        [Fact]
        public void BasketInfo_Verifiable()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Verifiable();

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

            HttpCookie StudentCookies = new HttpCookie("StudentCookies");
            StudentCookies.Value = "hallo";
            StudentCookies.Expires = DateTime.Now.AddHours(1);

            //_sut.HttpContext.Request.Cookies.Add(StudentCookies);
            _gameService.Setup((service => service.GetByKey(fakeGameKey))).Returns(fakeGame);

            var cookieCollection = new HttpCookieCollection { };
            var reqest = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            reqest.SetupGet(r => r.Cookies).Returns(cookieCollection);
            response.SetupGet(r => r.Cookies).Returns(cookieCollection);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Response).Returns(response.Object);
            context.SetupGet(x => x.Request).Returns(reqest.Object);


            _sut.ControllerContext = new ControllerContext(context.Object, new RouteData(), _sut);

            var res = _sut.AddGameToOrder(fakeGameKey);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void DeleteGameFromOrder_gameId_Verifiable()
        {
            var fakeUserId = Guid.NewGuid();
            var fakeGameId = Guid.NewGuid();

            _ordersService.Setup(service => service.DeleteGameFromOrder(fakeUserId, fakeGameId)).Verifiable();

            _sut.DeleteGameFromOrder(fakeGameId);

            _ordersService.Verify(s => s.DeleteGameFromOrder(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Pay_PaymentTypesBank_FileStreamResult()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Bank);

            Assert.Equal(typeof(FileStreamResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesBox_ReturnViewResult()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Box);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesVisa_ReturnViewResult()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Visa);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void CountGamesInOrder_ReturnPartialViewResult()
        {
            var res = _sut.CountGamesInOrder();

            Assert.Equal(typeof(PartialViewResult), res.GetType());
        }
    }
}
