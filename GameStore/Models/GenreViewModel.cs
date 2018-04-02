using System;

namespace GameStore.Models
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? IdParentGanre { get; set; }

        public string Name { get; set; }
    }
}
