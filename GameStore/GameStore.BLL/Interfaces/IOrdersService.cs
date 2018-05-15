using GameStore.BLL.DTO;
using System;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        OrderDTO GetOrder(Guid userId);

        void AddNewOrderDetails(Guid userId, Guid gameId);

        int CountGamesInOrder(Guid userId);

        void DeleteGameFromOrder(Guid userId, Guid gameId);
    }
}
