using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class ExtendPlatformTypeDTO
    {
        public Guid Id { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public  ICollection<GameDTO> Games { get; set; }
    }
}
