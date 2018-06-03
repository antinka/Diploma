using GameStore.BLL.Enums;
using GameStore.BLL.Filters.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameSortFilter : IPipeLine<IEnumerable<Game>>
    {
        private readonly SortType _sortOption;

        public GameSortFilter(SortType sortOption)
        {
            _sortOption = sortOption;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            switch (_sortOption)
            {
                case SortType.MostCommented:
                    return input.OrderByDescending(game => game.Comments.Count());            
                case SortType.MostPopular:
                    return input.OrderByDescending(game => game.Views);
                case SortType.NewByDate:
                    return input.OrderByDescending(game => game.PublishDate.Ticks);
                case SortType.PriceDesc:
                    return input.OrderByDescending(game => game.Price);
                case SortType.PriceAsc:
                    return input.OrderBy(game => game.Price);
                default:
                    return input;
            }
        }
    }
}
