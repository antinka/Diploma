using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string HomePage { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}