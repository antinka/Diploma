using GameStore.BLL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

namespace GameStore.ViewModels
{
    public class FilterViewModel
    {
        public SortDate SortDate { get; set; }

        public SortType SortType { get; set; }

        public PageSize PageSize { get; set; }

        public int Page { get; set; }

        public int TotalItems { get; set; }

        [MinLength(3)]
        public string SearchGameName { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public IEnumerable<CheckBox> ListGenres { get; set; }

        public IEnumerable<CheckBox> SelectedGenres { get; set; }

        public IEnumerable<string> SelectedGenresName { get; set; }

        public IEnumerable<CheckBox> ListPlatformTypes { get; set; }

        public IEnumerable<CheckBox> SelectedPlatformTypes { get; set; }

        public IEnumerable<string> SelectedPlatformTypesName { get; set; }

        public IEnumerable<CheckBox> ListPublishers { get; set; }

        public IEnumerable<CheckBox> SelectedPublishers { get; set; }

        public IEnumerable<string> SelectedPublishersName { get; set; }
    }
}