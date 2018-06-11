using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class ExtendGenreDTO
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        public GenreDTO ParentGenre { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public ICollection<GameDTO> Games { get; set; }
    }
}
