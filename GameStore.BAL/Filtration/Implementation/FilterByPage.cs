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
            int? itemsPerPage = null;

            if (_pageSize != PageSize.All)
            {
                itemsPerPage = (int)_pageSize;
            }

            var pageIndex = _page == 0 ? 1 : _page;

            if (itemsPerPage != null)
            {
                input.Skip(itemsPerPage.Value * (pageIndex - 1)).Take(itemsPerPage.Value);
            }

            return input;
        }
    }
}
