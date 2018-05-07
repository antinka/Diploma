using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.Infrastructure.Mapper;
using GameStore.Payments.Enums;
using GameStore.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrdersService> _ordersService;
        private readonly IMapper _mapper;
        private readonly OrderController _sut;

        public OrderControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _ordersService = new Mock<IOrdersService>();
            _sut = new OrderController(_ordersService.Object, _mapper);
        }

        [Fact]
        public void BasketInfo_ReturnViewResult()
        {
            var fakeUserId = Guid.Empty;
            var fakeOrder = new OrderDTO(){ Id = Guid.NewGuid(), UserId = fakeUserId };

            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Returns(fakeOrder);
            _ordersService.Setup(service => service.GetAllShippers()).Returns(new List<ShipperDTO>());

            var res = _sut.BasketInfo();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void AddGameToOrder_ValidBasketViewModel_Verifiable()
        {
            var fakeBasketViewModel = new BasketViewModel() { UserId = Guid.NewGuid(), GameId = Guid.NewGuid(), Quantity = 5 };

            _ordersService.Setup(service => service.AddNewOrderDetails(fakeBasketViewModel.UserId, fakeBasketViewModel.GameId, fakeBasketViewModel.Quantity)).Verifiable();

            _sut.AddGameToOrder(fakeBasketViewModel);

            _ordersService.Verify(s => s.AddNewOrderDetails(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<short>()), Times.Once);
        }

        [Fact]
        public void AddGameToOrder_InvalidBasketViewModel_ReturnViewResult()
        {
            var fakeBasketViewModel = new BasketViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.AddGameToOrder(fakeBasketViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
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
        public void UpdateShipper_OrderViewModel_ReturnedRedirectToRouteResult()
        {
            var fakeOrderViewModel = new OrderViewModel() {Id = Guid.NewGuid()};

            var res = _sut.UpdateShipper(fakeOrderViewModel) as RedirectToRouteResult;

            Assert.Equal("BasketInfo", res.RouteValues["action"]);
        }

        [Fact]
        public void FilterOrders_FilterOrder_ReturnedViewResult()
        {
            var fakeFilterOrder = new FilterOrder();

            var res = _sut.FilterOrders(fakeFilterOrder);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
