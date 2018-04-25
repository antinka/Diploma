using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.Infastracture;
using log4net;
using Moq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class OrdersServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly OrdersService _sut;
        private readonly IMapper _mapper;

        private readonly Guid _fakeUserId, _fakeGameId;
        private readonly Order _fakeOrder;
        private readonly Game _fakeGame;
        private readonly List<Order> _fakeOrders;

        public OrdersServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new OrdersService(_uow.Object, _mapper);

            _fakeUserId = Guid.NewGuid();
            _fakeGameId = Guid.NewGuid();

            _fakeOrder = new Order()
            {
                UserId = _fakeUserId
            };

            _fakeOrders = new List<Order>()
            {
                _fakeOrder
            };

            _fakeGame = new Game()
            {
                Id = _fakeGameId,
                Key = "123"
            };
        }

        [Fact]
        public void GetOrderDetail_ExistedUserId_OrderDetailReturned()
        {
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(_fakeOrders);

            var resultOrderDetelis = _sut.GetOrder(_fakeUserId);

            Assert.True(resultOrderDetelis.UserId == _fakeUserId);
        }

        [Fact]
        public void GetOrderDetail_NotExistedUserId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(null as IEnumerable<Order>);

            Assert.Throws<EntityNotFound>(() => _sut.GetOrder(_fakeUserId));
        }

        [Fact]
        public void AddNewOrderDetails_NotExistedGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId, 3));
        }

        [Fact]
        public void AddNewOrderDetails_NotExistedOrder_AddNewOrderAndOrderDetails()
        {
            var fakeOrderDetailsDto = new OrderDetailDTO() { Id = Guid.NewGuid(), Game = _mapper.Map<GameDTO>(_fakeGame), GameId = _fakeGameId};
            var fakeOrderDetail = _mapper.Map<OrderDetail>(fakeOrderDetailsDto);

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());
            _uow.Setup(uow => uow.OrderDetails.Create(fakeOrderDetail)).Verifiable();

            _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId, 3);

            _uow.Verify(uow => uow.OrderDetails.Create(It.IsAny<OrderDetail>()), Times.Once);
        }
    }
}
