using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByPlatform : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedPlatformTypes;

        public GameFilterByPlatform(IEnumerable<string> selectedPlatformTypes)
        {
            _selectedPlatformTypes = selectedPlatformTypes;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedPlatformTypes.Any())
            {
                return input.Where(game => Enumerable.Any(game.PlatformTypes, platform => _selectedPlatformTypes.Contains(platform.Name)));
            }

            return input;
        }
    }
}
