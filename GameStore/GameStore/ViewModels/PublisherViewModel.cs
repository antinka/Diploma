using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 200 characters and less than 3 characters")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Home page")]
        public string HomePage { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}