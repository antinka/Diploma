using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BAL.Interfaces
{
    public interface IGameCrudService
    {
        void AddNewGame(GameDTO gameDto);
        void EditGame(GameDTO gameDto);
        void DeleteGame(Guid id);
        GameDTO GetGame(Guid id);
        IEnumerable<GameDTO> GetAllGame();
    }
}
