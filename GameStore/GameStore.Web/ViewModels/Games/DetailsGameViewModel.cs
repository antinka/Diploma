using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels.Games
{
    public class DetailsGameViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Key", ResourceType = typeof(GlobalRes))]
        public string Key { get; set; }

        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(GlobalRes))]
        public string Description { get; set; }

        [Display(Name = "Price", ResourceType = typeof(GlobalRes))]
        public decimal Price { get; set; }

        [Display(Name = "UnitsInStock", ResourceType = typeof(GlobalRes))]
        public short UnitsInStock { get; set; }

        [Display(Name = "Discountinues", ResourceType = typeof(GlobalRes))]
        public bool Discountinues { get; set; }

        public Guid? PublisherId { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(GlobalRes))]
        public DetailsPublisherViewModel Publisher { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(GlobalRes))]
        public ICollection<CommentViewModel> Comments { get; set; }

        [Display(Name = "Genres", ResourceType = typeof(GlobalRes))]
        public ICollection<DelailsGenreViewModel> Genres { get; set; }

        [Display(Name = "PlatformTypes", ResourceType = typeof(GlobalRes))]
        public ICollection<DetailsPlatformTypeViewModel> PlatformTypes { get; set; }
    }
}