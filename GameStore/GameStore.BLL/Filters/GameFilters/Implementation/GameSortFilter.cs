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

            if (_sortOption == SortType.MostCommented)
                condition = game => game.Comments.Count()*-1;
            else if (_sortOption == SortType.MostPopular)
            {
                condition = game => game.Views * -1;
            }
            else if (_sortOption == SortType.NewByDate)
            {
                condition = game => game.PublishDate.Ticks*(-1);
            }
            else if (_sortOption == SortType.PriceDesc)
            {
                condition = game => game.Price * -1;
            }
            else if (_sortOption == SortType.PriceAsc)
            {
                condition = game => game.Price;
            }

            return condition != null ? input.OrderBy(condition) : input;
        }
    }
}
