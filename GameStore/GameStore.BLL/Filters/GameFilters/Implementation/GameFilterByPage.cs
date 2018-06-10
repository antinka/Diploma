using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Filters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByPage : IPipeLine<IEnumerable<Game>>
    {
        private readonly int _page;
        private readonly PageSize _pageSize;

        public GameFilterByPage(int page, PageSize pageSize)
        {
            _page = page;
            _pageSize = pageSize;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if ((int)_pageSize != int.MaxValue)
            {
                return input.Skip((_page - 1) * (int)_pageSize).Take((int)_pageSize);
            }

            return input;
        }
    }
}
