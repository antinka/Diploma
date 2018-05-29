using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class DelailsGenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        [Display(Name = "ParentGenreName", ResourceType = typeof(GlobalRes))]
        public string ParentGenreName { get; set; }

        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        public ICollection<FilterGameViewModel> Games { get; set; }

        public SelectList GenreList { get; set; }
    }
}
