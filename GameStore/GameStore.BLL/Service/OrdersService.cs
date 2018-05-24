using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Linq;
using GameStore.BLL.CustomExeption;
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

            orderDTO.Cost = orderDTO.OrderDetails.Sum(i => i.Price);

            return orderDTO;
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
                        if (orderDetails.IsDelete)
                            orderDetails.IsDelete = false;

                        orderDetails.Quantity += 1;
                        orderDetails.Price += game.Price;
                    }
                    else
                        CreateNewOrderDetailToExistOrder(order, game, gameId);
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

        public int CountGamesInOrder(Guid userId)
        {
            var order = _unitOfWork.Orders.Get(o => o.UserId == userId).FirstOrDefault();

            if (order != null)
                return order.OrderDetails.Aggregate(0, (current, game) => current + game.Quantity);

            return 0;
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
    }
}
