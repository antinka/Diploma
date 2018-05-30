using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class RoleDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserDTO> Users { get; set; }
    }
}
