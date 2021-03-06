﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using GameStore.BLL.Enums;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class FilterViewModel
    {
        public IEnumerable<DetailsGameViewModel> Games { get; set; }

        public PagingInfo PagingInfo { get; set; }

        [Display(Name = "FilterDate", ResourceType = typeof(GlobalRes))]
        public FilterDate FilterDate { get; set; }

        [Display(Name = "SortType", ResourceType = typeof(GlobalRes))]
        public SortType SortType { get; set; }

        [Display(Name = "PageSize", ResourceType = typeof(GlobalRes))]
        public PageSize PageSize { get; set; }

        [Display(Name = "SearchGameName", ResourceType = typeof(GlobalRes))]
        [MinLength(3)]
        public string SearchGameName { get; set; }

        [Display(Name = "MinPrice", ResourceType = typeof(GlobalRes))]
        public decimal? MinPrice { get; set; }

        [Display(Name = "MaxPrice", ResourceType = typeof(GlobalRes))]
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