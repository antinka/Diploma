using System;

namespace GameStore.ViewModels
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        public string Name { get; set; }
    }
}
