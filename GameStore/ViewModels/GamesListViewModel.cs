using System.Collections.Generic;
using GameStore.BLL.Enums;

namespace GameStore.ViewModels
{
    public class GamesListViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }

        public int TotalItems { get; set; }

        public PageSize ItemsPerPage { get; set; }

        public int Page { get; set; }
    }

}