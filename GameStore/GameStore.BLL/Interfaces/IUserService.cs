using System;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IUserService
    {
        void AddNew(UserDTO userDto);

        void Update(UserDTO userDto);

        void Delete(Guid id);

        UserDTO GetById(Guid id);

        bool IsUniqueName(UserDTO userDto);
    }
}
