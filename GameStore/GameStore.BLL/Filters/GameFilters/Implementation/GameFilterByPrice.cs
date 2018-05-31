using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class FilterByMaxPrice : IPipeLine<IEnumerable<Game>>
    {
        private readonly decimal _maxPrice;

        public FilterByMaxPrice(decimal maxPrice)
        {
            _maxPrice = maxPrice;
        }
        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            return input.Where(game => game.Price <= _maxPrice);
        }
    }

    public class FilterByMinPrice : IPipeLine<IEnumerable<Game>>
    {
        private readonly decimal _minPrice;

        public FilterByMinPrice(decimal minPrice)
        {
            _minPrice = minPrice;
        }
        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            return input.Where(game => game.Price >= _minPrice);
        }
    }
}
