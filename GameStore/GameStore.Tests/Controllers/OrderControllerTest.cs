﻿using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.Infrastructure.Mapper;
using GameStore.Payments.Enums;
using GameStore.ViewModels;
using Moq;
using System;
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
        public void BasketInfo_ReturnView()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Verifiable();

            _sut.BasketInfo();

            _ordersService.Verify(s => s.GetOrder(It.IsAny<Guid>()), Times.Once);
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
        public void AddGameToOrder_InvalidBasketViewModel_ReturnView()
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
        public void Pay_PaymentTypesBox_ViewResult()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Box);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Pay_PaymentTypesVisa_ViewResult()
        {
            var fakeUserId = Guid.Empty;
            _ordersService.Setup(service => service.GetOrder(fakeUserId)).Returns(new OrderDTO());

            var res = _sut.Pay(PaymentTypes.Visa);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
