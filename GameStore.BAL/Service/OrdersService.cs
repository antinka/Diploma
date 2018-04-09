using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public OrdersService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public OrderDTO GetOrderDetail(Guid userId)
        {
            var orders = _unitOfWork.Orders.Get(x => x.UserId == userId).FirstOrDefault();

            return _mapper.Map<OrderDTO>(orders);
        }

        public void AddNewOrderDetails(Guid userId, Guid gameId, short quantity)
        {
            var game = _unitOfWork.Games.GetById(gameId);
            var order = _unitOfWork.Orders.Get(x => x.UserId == userId).FirstOrDefault();

            if (order == null)
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
            else
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
        }
    }
}
