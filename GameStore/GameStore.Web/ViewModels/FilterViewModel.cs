using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using GameStore.BLL.Enums;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class FilterViewModel
    {
        public IEnumerable<DetailsGameViewModel> Games { get; set; }

        public PagingInfo PagingInfo { get; set; }

        [Display(Name = "Filter Date")]
        public FilterDate FilterDate { get; set; }

        [Display(Name = "Sort Type")]
        public SortType SortType { get; set; }

        [Display(Name = "Games per page")]
        public PageSize PageSize { get; set; }

        [Display(Name = "Search Game Name")]
        [RegularExpression(@"^[A-Za-z0-9_-]{3,200}", ErrorMessage = "Key cannot be longer than 200 characters and less than 3 characters and could contains only A-Za-z0-9")]
        public string SearchGameName { get; set; }

        [Display(Name = "Min Price")]
        [Range(0, 10000)]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Max Price")]
        [Range(0, 10000)]
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