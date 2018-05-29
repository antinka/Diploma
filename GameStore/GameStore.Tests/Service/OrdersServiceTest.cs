using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo.MongoEntities;
using GameStore.Web.Infrastructure.Mapper;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

            _fakeOrder = new Order()
            {
                UserId = _fakeUserId,
                OrderDetails = _fakeOrderDetails
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
            var fakeOrderDetailsDto = new OrderDetailDTO() { Id = Guid.NewGuid(), Game = _mapper.Map<ExtendGameDTO>(_fakeGame), GameId = _fakeGameId };
            var fakeOrderDetail = _mapper.Map<OrderDetail>(fakeOrderDetailsDto);

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());
            _uow.Setup(uow => uow.OrderDetails.Create(fakeOrderDetail)).Verifiable();

            _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId);

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

        [Fact]
        public void AddNewOrderDetails_ExistedOrder_Verifiable()
        {
            var fakeOrderDetailsDto = new OrderDetailDTO() { Id = Guid.NewGuid(), Game = _mapper.Map<ExtendGameDTO>(_fakeGame), GameId = _fakeGameId };
            var fakeOrderDetail = _mapper.Map<OrderDetail>(fakeOrderDetailsDto);
            var order = new Order() { Id = Guid.NewGuid(), OrderDetails = new List<OrderDetail>() { fakeOrderDetail } };
            var orders = new List<Order>() { order };

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(orders).Verifiable();

            _sut.AddNewOrderDetails(_fakeUserId, _fakeGameId);

            _uow.Verify(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>()), Times.Once);
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

        [Fact]
        public void CountGamesInOrder_UserIdWhichNotMakeOrderYet_NumberOfGamesInUserOrder0()
        {
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(new List<Order>());

            var res = _sut.CountGamesInOrder(_fakeUserId);

            Assert.Equal(0, res);
        }

        [Fact]
        public void CountGamesInOrder_UserId_NumberOfGamesInUserOrder()
        {
            _uow.Setup(uow => uow.Orders.Get(It.IsAny<Func<Order, bool>>())).Returns(_fakeOrders);
            var expect = _fakeOrders.FirstOrDefault().OrderDetails.Aggregate(0, (current, game) => current + game.Quantity);
            var res = _sut.CountGamesInOrder(_fakeUserId);

            Assert.Equal(expect, res);
        }
    }
}
