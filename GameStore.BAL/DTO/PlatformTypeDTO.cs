using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class PlatformTypeDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<GameDTO> Games { get; set; }
    }
}
