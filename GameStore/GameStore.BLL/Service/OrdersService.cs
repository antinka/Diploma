using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
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

			//todo use navigation prop here and everywhere. 
            order.OrderDetails = _unitOfWork.OrderDetails.Get(o => o.OrderId == order.Id).ToList();
            var orderDTO = _mapper.Map<OrderDTO>(order);

            foreach (var i in orderDTO.OrderDetails)
            {
				//todo use Sum
                orderDTO.Cost += i.Price;
            }

            return orderDTO;
        }

        public void AddNewOrderDetails(Guid userId, Guid gameId)
        {
            var game = _unitOfWork.Games.GetById(gameId);

            if (game != null)
            {
                var order = _unitOfWork.Orders.Get(o => o.UserId == userId).FirstOrDefault();

                if (order == null)
                {
                    CreateNewOrderWithOrderDetails(game, userId, gameId);
                }
                else
                {
					//todo I think if order detail is already payed or deleted, you should create new one
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

				//todo if order is not payed, you can't change UnitInStock
                game.UnitsInStock -= 1;
                _unitOfWork.Games.Update(game);
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
                        orderDetails.IsDelete = true;

                        var order = _unitOfWork.Orders.Get(o => o.UserId == userId).FirstOrDefault();
                        if (order != null && order.OrderDetails.All(o => o.IsDelete))
                            order.IsDelete = true;
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
                    UserId = userId,
                    Date = DateTime.UtcNow
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
