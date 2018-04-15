﻿using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class GameDTO
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discountinues { get; set; }

        public Guid? PublisherId { get; set; }

        public PublisherDTO Publisher { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }

        public ICollection<GenreDTO> Genres { get; set; }

        public IEnumerable<Guid> GenresId { get; set; }

        public ICollection<PlatformTypeDTO> PlatformTypes { get; set; }

        public IEnumerable<Guid> PlatformTypesId { get; set; }
    }
}
