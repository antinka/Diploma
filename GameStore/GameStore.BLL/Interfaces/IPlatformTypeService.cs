﻿using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        PlatformTypeDTO GetById(Guid id);

        IEnumerable<PlatformTypeDTO> GetAll();

        void AddNew(ExtendPlatformTypeDTO platformTypeDTO);

        void Update(ExtendPlatformTypeDTO platformTypeDto);

        void Delete(Guid id);

        ExtendPlatformTypeDTO GetByName(string name);

        bool IsUniqueEnName(ExtendPlatformTypeDTO platformTypeDTO);

        bool IsUniqueRuName(ExtendPlatformTypeDTO platformTypeDTO);
    }
}
