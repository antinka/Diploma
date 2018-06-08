using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class DetailsPublisherViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(GlobalRes))]
        public string Description { get; set; }

        [Display(Name = "HomePage", ResourceType = typeof(GlobalRes))]
        public string HomePage { get; set; }

        public ICollection<FilterGameViewModel> Games { get; set; }
    }
}