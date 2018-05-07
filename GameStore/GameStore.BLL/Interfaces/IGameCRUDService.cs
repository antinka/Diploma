using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameCrudService
    {
        bool AddNew(GameDTO gameDto);

        void Update(GameDTO gameDto);

        void Delete(Guid id);

        GameDTO GetById(Guid id);

        GameDTO GetByKey(string gamekey);

        IEnumerable<GameDTO> GetAll();
    }
}
