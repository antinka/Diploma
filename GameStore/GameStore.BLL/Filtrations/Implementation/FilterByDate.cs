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
        private DateTime _from;

        public FilterByDate(SortDate selectedDate)
        {
            _selectedDate = selectedDate;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {

            if (_selectedDate == SortDate.Week)
                _from = DateTime.UtcNow.AddDays(-7);
            else if (_selectedDate == SortDate.Month)
            {
                _from = DateTime.UtcNow.AddMonths(-1);
            }
            else if (_selectedDate == SortDate.OneYear)
            {
                _from = DateTime.UtcNow.AddYears(-1);
            }
            else if (_selectedDate == SortDate.TwoYear)
            {
                _from = DateTime.UtcNow.AddYears(-2);
            }
            else if (_selectedDate == SortDate.ThreeYear)
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
