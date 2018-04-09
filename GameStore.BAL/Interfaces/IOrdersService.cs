using GameStore.BLL.DTO;
using System;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        OrderDTO GetOrderDetail(Guid userId);

        void AddNewOrderDetails(Guid userId, Guid gameId, short quantity);
    }
}
