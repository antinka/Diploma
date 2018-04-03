using System;

namespace GameStore.ViewModels
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? IdParentGanre { get; set; }

        public string Name { get; set; }
    }
}
