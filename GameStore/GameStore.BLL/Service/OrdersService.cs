using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Linq;

namespace GameStore.BLL.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public OrderDTO GetOrder(Guid userId)
        {
            var order = _unitOfWork.Orders.Get(x => x.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                throw new EntityNotFound($"{nameof(OrdersService)} - Orders with such id user {userId} did not exist");
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
    }
}
