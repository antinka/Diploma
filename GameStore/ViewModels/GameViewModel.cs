using System;

namespace GameStore.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
