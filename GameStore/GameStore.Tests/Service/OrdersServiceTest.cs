using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo.MongoEntities;
using GameStore.Infrastructure.Mapper;
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
        private readonly Game _fakeGame;
        private readonly Order _fakeOrder;
        private readonly List<Order> _fakeOrders;

        public OrdersServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new OrdersService(_uow.Object, _mapper, log.Object);

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
        public void GetOrderDetail_NotExistedUserId_ReturnedEmptyOrder()
        {
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());

            Assert.Null(_sut.GetOrder(_fakeUserId));
        }

        [Fact]
        public void AddNewOrderDetails_NotExistedGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId, 3));
        }

        [Fact]
        public void AddNewOrderDetails_NotExistedOrder_Verifiable()
        {
            var fakeOrderDetailsDto = new OrderDetailDTO() { Id = Guid.NewGuid(), Game = _mapper.Map<GameDTO>(_fakeGame), GameId = _fakeGameId };
            var fakeOrderDetail = _mapper.Map<OrderDetail>(fakeOrderDetailsDto);

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());
            _uow.Setup(uow => uow.OrderDetails.Create(fakeOrderDetail)).Verifiable();

            _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId, 3);

            _uow.Verify(uow => uow.OrderDetails.Create(It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void GetAllShippers_AllShippersReturned()
        {
            var fakeShippers = new List<Shipper>()
            {
                new Shipper(),
                new Shipper()
            };
            _uow.Setup(uow => uow.Shippers.GetAll()).Returns(fakeShippers);

            var resultShippers = _sut.GetAllShippers();

            Assert.Equal(resultShippers.Count(), fakeShippers.Count);
        }

        [Fact]
        public void UpdateShipper_OrderDTO_Verifiable()
        {
            var fakeOrderDTO = _mapper.Map<OrderDTO>(_fakeOrder);

            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(_fakeOrders);

            _uow.Setup(uow => uow.Orders.Update(_fakeOrder)).Verifiable();

            _sut.UpdateShipper(fakeOrderDTO);

            _uow.Verify(uow => uow.Orders.Update(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void GetOrdersBetweenDates_ReturnedOrders()
        {
            _uow.Setup(uow => uow.Orders.GetAll()).Returns(_fakeOrders);

            var res = _sut.GetOrdersBetweenDates(null, null);

            Assert.Equal(res.Count(), _fakeOrders.Count);
        }

        [Fact]
        public void GetOrdersBetweenDates_FromDate_ReturnedOrders()
        {
            var fakeOrders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today.AddMonths(-1)
                    
                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today.AddMonths(-2)
                }
            };
            _uow.Setup(uow => uow.Orders.GetAll()).Returns(fakeOrders);

            var res = _sut.GetOrdersBetweenDates(DateTime.Today.AddMonths(-1), null);

            Assert.Equal(res.Count(), fakeOrders.Count-1);
        }

        [Fact]
        public void GetOrdersBetweenDates_ToDate_ReturnedOrders()
        {
            var fakeOrders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today

                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today.AddMonths(-2)
                }
            };
            _uow.Setup(uow => uow.Orders.GetAll()).Returns(fakeOrders);

            var res = _sut.GetOrdersBetweenDates(null, DateTime.Today.AddMonths(-1));

            Assert.Equal(res.Count(), fakeOrders.Count - 1);
        }

        [Fact]
        public void GetOrdersBetweenDates_FromDateToDate_ReturnedOrders()
        {
            var fakeOrders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today.AddMonths(-1)

                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today.AddMonths(-2)
                },
                new Order()
                {

                    Id = Guid.NewGuid(),
                    Date =  DateTime.Today.AddMonths(-3)
                }
            };
            _uow.Setup(uow => uow.Orders.GetAll()).Returns(fakeOrders);

            var res = _sut.GetOrdersBetweenDates(DateTime.Today.AddMonths(-3), DateTime.Today);

            Assert.Equal(res.Count(), fakeOrders.Count);
        }
    }
}
