using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        OrderDTO GetOrder(Guid userId);

        IEnumerable<OrderDTO> GetOrdersBetweenDates(DateTime? from, DateTime? to);

        IEnumerable<ShipperDTO> GetAllShippers();

        void UpdateShipper(OrderDTO orderDto);

        void AddNewOrderDetails(Guid userId, Guid gameId);

        int CountGamesInOrder(Guid userId);

        void DeleteGameFromOrder(Guid userId, Guid gameId);
    }
}
