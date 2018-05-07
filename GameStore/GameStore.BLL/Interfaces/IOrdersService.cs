using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        OrderDTO GetOrder(Guid userId);

        void AddNewOrderDetails(Guid userId, Guid gameId, short quantity);

        IEnumerable<OrderDTO> GetOrdersBetweenDates(DateTime? from, DateTime? to);

        IEnumerable<ShipperDTO> GetAllShippers();

        void UpdateShipper(OrderDTO orderDto);
    }
}
