using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        OrderDTO GetOrderByUserId(Guid userId);

        IEnumerable<OrderDTO> GetOrdersWithUnpaidBetweenDates(DateTime? from, DateTime? to);

        IEnumerable<OrderDTO> GetOrdersBetweenDates(DateTime? from, DateTime? to);

        IEnumerable<ShipperDTO> GetAllShippers();

        void UpdateShipper(OrderDTO orderDto);

        void UpdateOrder(OrderDTO orderDto);

        void AddNewOrderDetails(Guid userId, Guid gameId);

        int CountGamesInOrder(Guid userId);

        void DeleteGameFromOrder(Guid userId, Guid gameId);

        void Pay(Guid orderId);
    }
}
