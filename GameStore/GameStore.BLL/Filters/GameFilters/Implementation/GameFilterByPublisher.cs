using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByPublisher : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedPublishers;

        public GameFilterByPublisher(IEnumerable<string> selectedPublishers)
        {
            _selectedPublishers = selectedPublishers;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedPublishers.Count() != 0)
            {
                return input.Where(x => x.Publisher != null ? _selectedPublishers.Contains(x.Publisher.Name) : x.Name == "");
            }
            return input;
        }
    }
}

