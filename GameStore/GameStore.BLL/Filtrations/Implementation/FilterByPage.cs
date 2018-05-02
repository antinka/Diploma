using GameStore.BLL.Enums;
using GameStore.BLL.Filtration.Interfaces;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtration.Implementation
{
    public class FilterByPage : IPipeLine<IEnumerable<Game>>
    {
        private readonly int _page;
        private readonly PageSize _pageSize;

        public FilterByPage(int page, PageSize pageSize)
        {
            _page = page;
            _pageSize = pageSize;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            int size = (int) _pageSize;

            if (size != 0)
            {
                return input.Skip((_page - 1) * size) .Take(size);
            }

            return input;
        }
    }
}
