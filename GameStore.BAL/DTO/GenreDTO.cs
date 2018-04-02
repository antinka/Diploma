using System;

namespace GameStore.BAL.DTO
{
    public class GenreDTO
    {
        public Guid Id { get; set; }

        public Guid? IdParentGanre { get; set; }

        public string Name { get; set; }
    }
}
