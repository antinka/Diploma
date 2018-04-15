using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameCrudService
    {
        void AddNew(GameDTO gameDto);

        void Update(GameDTO gameDto);

        void Delete(string gamekey);

        GameDTO Get(Guid id);

        IEnumerable<GameDTO> GetAll();
    }
}
