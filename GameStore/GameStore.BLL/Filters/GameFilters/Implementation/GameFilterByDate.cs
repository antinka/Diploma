using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Filters.GameFilters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByDate : IPipeLine<IEnumerable<Game>>
    {
        private readonly FilterDate _selectedDate;
        private DateTime _from;

        public GameFilterByDate(FilterDate selectedDate)
        {
            _selectedDate = selectedDate;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {

            if (_selectedDate == FilterDate.week)
                _from = DateTime.UtcNow.AddDays(-7);
            else if (_selectedDate == FilterDate.month)
            {
                _from = DateTime.UtcNow.AddMonths(-1);
            }
            else if (_selectedDate == FilterDate.oneYear)
            {
                _from = DateTime.UtcNow.AddYears(-1);
            }
            else if (_selectedDate == FilterDate.twoYear)
            {
                _from = DateTime.UtcNow.AddYears(-2);
            }
            else if (_selectedDate == FilterDate.threeYear)
            {
                _from = DateTime.UtcNow.AddYears(-3);
            }
            else
            {
                _from = DateTime.MinValue;
            }

            return input.Where(g=>g.PublishDate >= _from && g.PublishDate < DateTime.UtcNow);
        }
    }
}
