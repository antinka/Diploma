using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByPrice : IPipeLine<IEnumerable<Game>>
    {
        private readonly decimal? _maxPrice;
        private readonly decimal? _minPrice;

        public GameFilterByPrice(decimal? maxPrice, decimal? minPrice)
        {
            _maxPrice = maxPrice;
            _minPrice = minPrice;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_minPrice != null && _maxPrice != null)
            {
                return input.Where(x => x.Price >= _minPrice && x.Price <= _maxPrice);
            }

            if (_minPrice != null)
            {
                return input.Where(x => x.Price >= _minPrice);
            }

            if (_maxPrice != null)
            {
                return input.Where(x => x.Price <= _maxPrice);
            }

            return input;
        }
    }
}

