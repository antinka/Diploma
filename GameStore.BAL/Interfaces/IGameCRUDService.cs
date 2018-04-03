using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameCrudService
    {
        void AddNewGame(GameDTO gameDto);

        void UpdateGame(GameDTO gameDto);

        void DeleteGame(Guid id);

        GameDTO GetGame(Guid id);

        IEnumerable<GameDTO> GetAllGame();
    }
}
