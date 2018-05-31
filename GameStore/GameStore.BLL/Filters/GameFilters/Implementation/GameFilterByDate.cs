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
            switch (_selectedDate)
            {
                case FilterDate.week:
                    _from = DateTime.UtcNow.AddDays(-7);
                    break;
                case FilterDate.month:
                    _from = DateTime.UtcNow.AddMonths(-1);
                    break;
                case FilterDate.oneYear:
                    _from = DateTime.UtcNow.AddYears(-1);
                    break;
                case FilterDate.twoYear:
                    _from = DateTime.UtcNow.AddYears(-2);
                    break;
                case FilterDate.threeYear:
                    _from = DateTime.UtcNow.AddYears(-3);
                    break;
                default:
                    _from = DateTime.MinValue;
                    break;
            }

            return input.Where(g => g.PublishDate >= _from && g.PublishDate < DateTime.UtcNow);
        }
    }
}
