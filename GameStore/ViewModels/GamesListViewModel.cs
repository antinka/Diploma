using System;
using System.Collections.Generic;
using GameStore.BLL.Enums;

namespace GameStore.ViewModels
{
    public class GamesListViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public int TotalItems { get; set; }

        public PageSize ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)ItemsPerPage);
    }
}