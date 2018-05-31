using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Filters.GameFilters.Interfaces;
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
            int size = 0;

            switch (_pageSize)
            {
                case PageSize.OneHundred:
                    size = 100;
                    break;
                case PageSize.Fifty:
                    size = 50;
                    break;
                case PageSize.Twenty:
                    size = 20;
                    break;
                case PageSize.Ten:
                    size = 10;
                    break;
            }

            if (size != 0)
            {
                return input.Skip((_page - 1) * size).Take(size);
            }

            return input;
        }
    }
}
