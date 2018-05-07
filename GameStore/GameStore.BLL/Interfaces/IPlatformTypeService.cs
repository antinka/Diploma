using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        PlatformTypeDTO GetById(Guid id);

        IEnumerable<PlatformTypeDTO> GetAll();

        bool AddNew(PlatformTypeDTO platformTypeDto);

        void Update(PlatformTypeDTO platformTypeDto);

        void Delete(Guid id);

        PlatformTypeDTO GetByName(string name);
    }
}
