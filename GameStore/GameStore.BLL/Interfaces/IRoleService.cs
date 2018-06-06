using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IRoleService
    {
        void AddNew(RoleDTO roleDto);

        void Update(RoleDTO roleDto);

        void Delete(Guid id);

        RoleDTO GetById(Guid id);

        RoleDTO GetByName(string name);

        bool IsUniqueName(RoleDTO roleDto);

        IEnumerable<RoleDTO> GetAll();
    }
}
