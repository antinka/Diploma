using System;
using GameStore.BLL.Enums;
using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class SortFilter : IPipeLine<IEnumerable<Game>>
    {
        private readonly SortType _sortOption;

        public SortFilter(SortType sortOption)
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
                condition = game => game.Views;
            }
            else if (_sortOption == SortType.NewByDate)
            {
                condition = game => game.PublishDate.Ticks;
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
