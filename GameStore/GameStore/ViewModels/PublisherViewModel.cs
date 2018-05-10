using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.App_LocalResources;

namespace GameStore.ViewModels
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(GlobalRes))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "HomePage", ResourceType = typeof(GlobalRes))]
        public string HomePage { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}