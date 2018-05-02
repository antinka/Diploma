using GameStore.BLL.Enums;
using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class FilterByDate : IPipeLine<IEnumerable<Game>>
    {
        private readonly SortDate _selectedDate;

        public FilterByDate(SortDate selectedDate)
        {
            _selectedDate = selectedDate;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            Func<Game, object> condition = null;

            if (_selectedDate == SortDate.week)
                condition = game => game.PublishDate >= DateTime.Today.AddDays(-7);
            else if (_selectedDate == SortDate.month)
            {
                condition = game => game.PublishDate >= DateTime.Today.AddMonths(-1);
            }
            else if (_selectedDate == SortDate.oneYear)
            {
                condition = game => game.PublishDate >= DateTime.Today.AddYears(-1);
            }
            else if (_selectedDate == SortDate.twoYear)
            {
                condition = game => game.PublishDate >= DateTime.Today.AddYears(-2);
            }
            else if (_selectedDate == SortDate.threeYear)
            {
                condition = game => game.PublishDate >= DateTime.Today.AddYears(-3);
            }

            return condition != null ? input.OrderBy(condition) : input;
        }
    }
}
