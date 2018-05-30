using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class FilterByName : IPipeLine<IEnumerable<Game>>
    {
        private readonly string _gameName;

        public FilterByName(string gameName)
        {
            _gameName = gameName;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            return input.Where(game => game.NameEn.ToLower().Contains(_gameName.ToLower()) || game.NameRu.ToLower().Contains(_gameName.ToLower()));
        }
    }
}
