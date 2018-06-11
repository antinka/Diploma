using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public OrdersService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public OrderDTO GetOrderByOrderId(Guid orderId)
        {
            var order = _unitOfWork.Orders.Get(x => x.Id == orderId).FirstOrDefault();

            if (order == null)
            {
                _log.Info($"{nameof(OrdersService)} - Orders with such id order {orderId} did not exist");

                return null;
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);

            orderDTO.Cost = orderDTO.OrderDetails.Sum(i => i.Price);

            return orderDTO;
        }

        public OrderDTO GetOrderByUserId(Guid userId)
        {
            var order = _unitOfWork.Orders.Get(x => x.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                _log.Info($"{nameof(OrdersService)} - Orders with such id user {userId} did not exist");

                return null;
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);

            orderDTO.Cost = orderDTO.OrderDetails.Sum(i => i.Price);

            return orderDTO;
        }

        public void UpdateOrder(OrderDTO orderDto)
        {
            var order = _unitOfWork.Orders.Get(x => x.Id == orderDto.Id).FirstOrDefault();

            if (order == null)
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - Orders with such id {orderDto.Id} did not exist");
            }

            order = _mapper.Map<Order>(orderDto);

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        public void AddNewOrderDetails(Guid userId, Guid gameId)
        {
            var game = _unitOfWork.Games.GetById(gameId);

            if (game != null)
            {
                var order = _unitOfWork.Orders.Get(o => o.UserId == userId && o.IsPaid == false).FirstOrDefault();

                if (order == null)
                {
                    CreateNewOrderWithOrderDetails(game, userId, gameId);
                }
                else
                {
                    var orderDetails = order.OrderDetails.FirstOrDefault(o => o.GameId == game.Id);

                    if (orderDetails != null)
                    {
                        orderDetails.Quantity += 1;
                        orderDetails.Price += game.Price;
                    }
                    else
                    {
                        CreateNewOrderDetailToExistOrder(order, game, gameId);
                    }
                }
            
                _unitOfWork.Save();
                _log.Info($"{nameof(OrdersService)} - User {userId} add game {game.Key} to order");
            }
            else
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - game with such id  {gameId} did not exist");
            }
        }

        public void DeleteGameFromOrder(Guid userId, Guid gameId)
        {
            var game = _unitOfWork.Games.GetById(gameId);

            if (game != null)
            {
                var orderDetails = _unitOfWork.OrderDetails
                    .Get(g => g.Game.Key == game.Key && g.Order.UserId == userId).FirstOrDefault();

                if (orderDetails != null)
                {
                    orderDetails.Quantity -= 1;
                    orderDetails.Price -= game.Price;
                    game.UnitsInStock += 1;

                    _unitOfWork.OrderDetails.Update(orderDetails);
                    _unitOfWork.Games.Update(game);

                    if (orderDetails.Quantity == 0)
                    {
                        _unitOfWork.OrderDetails.Delete(orderDetails.Id);
                    }

                    _unitOfWork.Save();

                    _log.Info($"{nameof(OrdersService)} - User {userId} delete game {game.Key} from order");
                }
            }
            else
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - game with such id {gameId} did not exist");
            }
        }

        public void Pay(Guid orderId)
        {
            var order = _unitOfWork.Orders.Get(x => x.Id == orderId).FirstOrDefault();

            if (order == null)
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - Orders with such id {orderId} did not exist");
            }

            order.IsPaid = true;
            order.Date = DateTime.UtcNow;
            order.ShipVia = _unitOfWork.Shippers.Get(s => s.Id == order.ShipperId).FirstOrDefault().ShipperID;

            foreach (var orderDetail in order.OrderDetails)
            {
                var game = orderDetail.Game;
                game.UnitsInStock -= orderDetail.Quantity;

                _unitOfWork.Games.Update(game);
            }

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        public int CountGamesInOrder(Guid userId)
        {
            var order = _unitOfWork.Orders.Get(o => o.UserId == userId && o.IsPaid == false).FirstOrDefault();

            if (order != null)
            {
                return order.OrderDetails.Aggregate(0, (current, game) => current + game.Quantity);
            }

            return 0;
        }

        public IEnumerable<OrderDTO> GetOrdersBetweenDates(DateTime? from, DateTime? to)
        {
            IEnumerable<Order> orders;

            if (from != null && to != null)
            {
                orders = _unitOfWork.Orders.GetAll().Where(x => x.Date >= from && x.Date <= to);
            }
            else if (from != null)
            {
                orders = _unitOfWork.Orders.GetAll().Where(x => x.Date >= from);
            }
            else if (to != null)
            {
                orders = _unitOfWork.Orders.GetAll().Where(x => x.Date <= to);
            }
            else
            {
                orders = _unitOfWork.Orders.GetAll();
            }

            var ordersDTO = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
            var shippers = _unitOfWork.Shippers.GetAll();

            foreach (var orderDTO in ordersDTO)
            {
                if (orderDTO.ShipVia != null)
                {
                    orderDTO.ShipViaName = shippers.Single(s => s.ShipperID == orderDTO.ShipVia).CompanyName;
                }

                foreach (var i in orderDTO.OrderDetails)
                {
                    orderDTO.Cost += i.Price;
                }
            }

            return ordersDTO;
        }

        public IEnumerable<OrderDTO> GetOrdersWithUnpaidBetweenDates(DateTime? from, DateTime? to)
        {
            IEnumerable<Order> orders;

            if (from != null && to != null)
            {
                orders = _unitOfWork.Orders.GetAll().Where(x => (x.Date >= from && x.Date <= to) || x.Date == null);
            }
            else if (from != null)
            {
                orders = _unitOfWork.Orders.GetAll().Where(x => x.Date >= from || x.Date == null);
            }
            else if (to != null)
            {
                orders = _unitOfWork.Orders.GetAll().Where(x => x.Date <= to || x.Date == null);
            }
            else
            {
                orders = _unitOfWork.Orders.GetAll();
            }

            var ordersDTO = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);

            foreach (var orderDTO in ordersDTO)
            {
                foreach (var i in orderDTO.OrderDetails)
                {
                    orderDTO.Cost += i.Price;
                }
            }

            return ordersDTO;
        }

        public IEnumerable<ShipperDTO> GetAllShippers()
        {
            var shippers = _unitOfWork.Shippers.GetAll();

            return _mapper.Map<IEnumerable<ShipperDTO>>(shippers);
        }

        public void UpdateShipper(OrderDTO orderDto)
        {
            var order = _unitOfWork.Orders.Get(x => x.Id == orderDto.Id).FirstOrDefault();

            if (order == null)
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - Orders with such id {orderDto.Id} did not exist");
            }

            order.ShipperId = orderDto.ShipperId;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        private void CreateNewOrderDetailToExistOrder(Order order, Game game, Guid gameId)
        {
            var orderDetail = new OrderDetailDTO()
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                Price = game.Price,
                Quantity = 1,
            };

            order.OrderDetails.Add(_mapper.Map<OrderDetail>(orderDetail));
        }

        private void CreateNewOrderWithOrderDetails(Game game, Guid userId, Guid gameId)
        {
            var orderDetail = new OrderDetailDTO()
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                Price = game.Price,
                Quantity = 1,
                Order = new OrderDTO()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                }
            };

            _unitOfWork.OrderDetails.Create(_mapper.Map<OrderDetail>(orderDetail));
        }
    }
}
