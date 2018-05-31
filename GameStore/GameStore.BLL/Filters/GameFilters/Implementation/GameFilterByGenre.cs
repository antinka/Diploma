using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByGenre : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedGenresName;

        public GameFilterByGenre(IEnumerable<string> selectedGenresName)
        {
            _selectedGenresName = selectedGenresName;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedGenresName.Count() != 0)
            {
                return input.Where(game => Enumerable.Any(game.Genres, genre => _selectedGenresName.Contains(genre.Name)));
            }

            return input;
        }
    }
}
