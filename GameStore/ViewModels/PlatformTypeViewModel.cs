using System;
using System.Collections.Generic;

namespace GameStore.ViewModels
{
    public class PlatformTypeViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}
