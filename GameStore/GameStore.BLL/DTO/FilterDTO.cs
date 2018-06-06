using GameStore.BLL.Enums;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class FilterDTO
    {
        public FilterDate FilterDate { get; set; }

        public SortType SortType { get; set; }

        public string SearchGameName { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public IEnumerable<string> SelectedGenresName { get; set; }

        public IEnumerable<string> SelectedPlatformTypesName { get; set; }

        public IEnumerable<string> SelectedPublishersName { get; set; }
    }
}
