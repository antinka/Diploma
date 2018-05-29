using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class FilterByGenre : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedGenresName;

        public FilterByGenre(IEnumerable<string> selectedGenresName)
        {
            _selectedGenresName = selectedGenresName;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedGenresName.Count() != 0)
            {
                return input.Where(game => Enumerable.Any(game.Genres, genre => _selectedGenresName.Contains(genre.NameEn)));
            }

            return input;
        }
    }
}
