using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class PublisherDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public ICollection<GameDTO> Games { get; set; }
    }
}
