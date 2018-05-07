using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public OrderDTO GetOrder(Guid userId)
        {
            var order = _unitOfWork.Orders.Get(x => x.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                _log.Info($"{nameof(OrdersService)} - Orders with such id user {userId} did not exist");

                return null;
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);

            foreach (var i in orderDTO.OrderDetails)
            {
                orderDTO.Cost += i.Price;
            }

            return orderDTO;
        }

        public void AddNewOrderDetails(Guid userId, Guid gameId, short quantity)
        {
            var game = _unitOfWork.Games.GetById(gameId);

            if (game != null)
            {
                var order = _unitOfWork.Orders.Get(x => x.UserId == userId).FirstOrDefault();

                if (order == null)
                {
                    CreateNewOrderWithOrderDetails(game, userId, gameId, quantity);
                }
                else
                {
                    CreateNewOrderDetailToExistOrder(order, game, gameId, quantity);
                }
            }
            else
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - game with such id  {gameId} did not exist");
            }
        }

        private void CreateNewOrderWithOrderDetails(Game game, Guid userId, Guid gameId, short quantity)
        {
            var orderDetail = new OrderDetailDTO()
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                Price = game.Price * quantity,
                Quantity = quantity,
                Order = new OrderDTO()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Date = DateTime.UtcNow
                }
            };

            _unitOfWork.OrderDetails.Create(_mapper.Map<OrderDetail>(orderDetail));
            _unitOfWork.Save();
        }

        private void CreateNewOrderDetailToExistOrder(Order order, Game game, Guid gameId, short quantity)
        {
            var orderDetail = new OrderDetailDTO()
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                Price = game.Price * quantity,
                Quantity = quantity,
            };

            order.OrderDetails.Add(_mapper.Map<OrderDetail>(orderDetail));
            _unitOfWork.Save();
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
    }
}
