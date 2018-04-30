using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class PlatformTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string Name { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}
