using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 40 characters and less than 3 characters")]
        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(GlobalRes))]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "HomePage", ResourceType = typeof(GlobalRes))]
        public string HomePage { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}