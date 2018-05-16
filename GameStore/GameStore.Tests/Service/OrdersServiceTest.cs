using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Web.Infrastructure.Mapper;
using Xunit;

namespace GameStore.Tests.Service
{
    public class OrdersServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly OrdersService _sut;
        private readonly IMapper _mapper;

        private readonly Guid _fakeUserId, _fakeGameId;
        private readonly Game _fakeGame;
        private readonly List<Order> _fakeOrders;
        private readonly List<OrderDetail> _fakeOrderDetails;

        public OrdersServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new OrdersService(_uow.Object, _mapper, log.Object);

            _fakeUserId = Guid.NewGuid();
            _fakeGameId = Guid.NewGuid();

             _fakeOrderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    GameId = _fakeGameId
                }
            };

            var fakeOrder = new Order()
            {
                UserId = _fakeUserId,
                OrderDetails = _fakeOrderDetails
            };

            _fakeOrders = new List<Order>()
            {
                fakeOrder
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
            _uow.Setup(uow => uow.OrderDetails.Get(It.IsAny<Func<OrderDetail, bool>>())).Returns(_fakeOrderDetails);

            var resultOrderDetelis = _sut.GetOrder(_fakeUserId);

            Assert.True(resultOrderDetelis.UserId == _fakeUserId);
        }

        [Fact]
        public void GetOrderDetail_NotExistedUserId_ReturnedEmptyOrder()
        {
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());

            Assert.Null(_sut.GetOrder(_fakeUserId));
        }

        [Fact]
        public void AddNewOrderDetails_NotExistedGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId));
        }

        [Fact]
        public void AddNewOrderDetails_NotExistedOrder_Verifiable()
        {
            var fakeOrderDetailsDto = new OrderDetailDTO() { Id = Guid.NewGuid(), Game = _mapper.Map<GameDTO>(_fakeGame), GameId = _fakeGameId};
            var fakeOrderDetail = _mapper.Map<OrderDetail>(fakeOrderDetailsDto);

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());
            _uow.Setup(uow => uow.OrderDetails.Create(fakeOrderDetail)).Verifiable();

            _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId);

            _uow.Verify(uow => uow.OrderDetails.Create(It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void DeleteGameFromOrder_NotExistedGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId));
        }

        [Fact]
        public void DeleteGameFromOrder_ExistedOrderIdAndUserId_Verifiable()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.OrderDetails.Get(It.IsAny<Func<OrderDetail, bool>>())).Returns(_fakeOrderDetails);
            _uow.Setup(uow => uow.Games.Update(_fakeGame)).Verifiable();
            _uow.Setup(uow => uow.OrderDetails.Update(_fakeOrderDetails.FirstOrDefault())).Verifiable();

            _sut.DeleteGameFromOrder(_fakeUserId, _fakeGameId);

            _uow.Verify(uow => uow.Games.Update(It.IsAny<Game>()), Times.Once);
            _uow.Verify(uow => uow.OrderDetails.Update(It.IsAny<OrderDetail>()), Times.Once);
        }
    }
}
