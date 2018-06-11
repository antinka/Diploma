using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class ExtendPublisherDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionRu { get; set; }

        public string HomePage { get; set; }

        public ICollection<GameDTO> Games { get; set; }
    }
}
