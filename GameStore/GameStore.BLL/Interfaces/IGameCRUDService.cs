using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameCrudService
    {
        void AddNew(ExtendGameDTO gameDto);

        void Update(ExtendGameDTO gameDto);

        void Delete(Guid id);

        GameDTO GetById(Guid id);

        ExtendGameDTO GetByKey(string gamekey);

        IEnumerable<GameDTO> GetAll();
    }
}
