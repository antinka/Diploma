using System;
using System.Collections.Generic;

namespace GameStore.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

        public ICollection<PlatformTypeViewModel> PlatformTypes { get; set; }
    }
}
