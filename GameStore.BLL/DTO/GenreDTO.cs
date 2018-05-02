using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class GenreDTO
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        public string ParentGenreName { get; set; }

        public string Name { get; set; }

        public ICollection<GameDTO> Games { get; set; }
    }
}
