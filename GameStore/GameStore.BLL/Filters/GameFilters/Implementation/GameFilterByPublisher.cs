using System;
using System.Collections;
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
            return input.Where(game => game.Publisher != null && _selectedPublishers.Contains(game.Publisher.Name));
        }
    }
}

