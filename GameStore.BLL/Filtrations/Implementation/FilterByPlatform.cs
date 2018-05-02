using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class FilterByPlatform : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedPlatformTypes;

        public FilterByPlatform(IEnumerable<string> selectedPlatformTypes)
        {
            _selectedPlatformTypes = selectedPlatformTypes;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedPlatformTypes.Count() != 0)
            {
                return input.Where(game => Enumerable.Any(game.PlatformTypes, platform => _selectedPlatformTypes.Contains(platform.Name)));
            }

            return input;
        }
    }
}
