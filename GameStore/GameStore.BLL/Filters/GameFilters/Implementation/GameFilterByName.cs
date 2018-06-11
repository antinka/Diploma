using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByName : IPipeLine<IEnumerable<Game>>
    {
        private readonly string _gameName;

        public GameFilterByName(string gameName)
        {
            _gameName = gameName;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            return input.Where(game => game.NameEn.ToLower().Contains(_gameName.ToLower()) || game.NameRu.ToLower().Contains(_gameName.ToLower()));
        }
    }
}
