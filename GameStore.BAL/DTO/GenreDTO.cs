using System;

namespace GameStore.BLL.DTO
{
    public class GenreDTO
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        public string Name { get; set; }
    }
}
