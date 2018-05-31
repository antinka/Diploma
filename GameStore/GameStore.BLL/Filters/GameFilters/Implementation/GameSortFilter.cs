using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

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
            Func<Game, object> condition = null;

            switch (_sortOption)
            {
                case SortType.MostCommented:
                    return input.OrderByDescending(game => game.Comments.Count());            
                case SortType.MostPopular:
                    condition = game => game.Views * -1;
                    break;
                case SortType.NewByDate:
                    condition = game => game.PublishDate.Ticks*(-1);
                    break;
                case SortType.PriceDesc:
                    condition = game => game.Price * -1;
                    break;
                case SortType.PriceAsc:
                    condition = game => game.Price;
                    break;
            }

            return condition != null ? input.OrderBy(condition) : input;
        }
    }
}
