using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class DetailsPlatformTypeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        public ICollection<FilterGameViewModel> Games { get; set; }
    }
}
