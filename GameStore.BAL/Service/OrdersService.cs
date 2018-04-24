using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Linq;
using GameStore.BLL.Exeption;

namespace GameStore.BLL.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
		//todo log?
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public OrdersService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

		//todo GetOrderDetail return Order, not order details
        public OrderDTO GetOrderDetail(Guid userId)
        {
            var orders = _unitOfWork.Orders.Get(x => x.UserId == userId);

            if (orders == null)
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - Orders with such id user {userId} did not exist");
            }
			//todo else
            else
            {
                return _mapper.Map<OrderDTO>(orders.FirstOrDefault());
            }
        }

        public void AddNewOrderDetails(Guid userId, Guid gameId, short quantity)
        {
			//todo simplify, too big method. hint: you could use several private methods
            var game = _unitOfWork.Games.GetById(gameId);

            if (game != null)
            {
                var order = _unitOfWork.Orders.Get(x => x.UserId == userId);

				//todo are u sure that here could be null?
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

                    order.First().OrderDetails.Add(_mapper.Map<OrderDetail>(orderDetail));
                    _unitOfWork.Save();
                }
            }
            else
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - game with such id  {gameId} did not exist");
            }
        }
    }
}
