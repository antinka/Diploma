using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using GameStore.BLL.Enums;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class FilterViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }

        public PagingInfo PagingInfo { get; set; }

        [Display(Name = "Sort Date")]
        public SortDate SortDate { get; set; }

        [Display(Name = "Sort Type")]
        public SortType SortType { get; set; }

        [Display(Name = "Page Size")]
        public PageSize PageSize { get; set; }

        [Display(Name = "Search Game Name")]
        [MinLength(3)]
        public string SearchGameName { get; set; }

        [Display(Name = "Min Price")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Max Price")]
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