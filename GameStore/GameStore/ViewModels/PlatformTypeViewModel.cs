using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.App_LocalResources;

namespace GameStore.ViewModels
{
    public class PlatformTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(450)]
        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}
