using System;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IRoleService
    {
        void AddNew(RoleDTO roleDto);

        void Update(RoleDTO roleDto);

        void Delete(Guid id);

        RoleDTO GetById(Guid id);

        bool IsUniqueName(RoleDTO roleDto);
    }
}
