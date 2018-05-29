using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class FilterByPublisher : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedPublishers;

        public FilterByPublisher(IEnumerable<string> selectedPublishers)
        {
            _selectedPublishers = selectedPublishers;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedPublishers.Count() != 0)
            {
                return input.Where(x => x.Publisher != null ? _selectedPublishers.Contains(x.Publisher.Name) : x.NameEn == "");
            }
            return input;
        }
    }
}

