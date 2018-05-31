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

            _selectedPublishers = selectedPublishers as IList<string> ?? selectedPublishers.ToList();
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            Func<Game, bool> condition = null;

            if (_selectedPublishers.Any())
            {
                condition = p => _selectedPublishers.Any(name => name == p.Publisher.Name);
            }

            return condition != null ? input.Where(condition) : input;
        }
    }
}

