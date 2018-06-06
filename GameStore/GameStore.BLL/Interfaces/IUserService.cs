using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IUserService
    {
        void AddNew(UserDTO userDto);

        void Update(UserDTO userDto);

        void Delete(Guid id);

        UserDTO GetById(Guid id);

        UserDTO GetByName(string name);

        UserDTO Login(string name, string password);

        bool IsUniqueName(UserDTO userDto);

        IEnumerable<UserDTO> GetAll();
    }
}
