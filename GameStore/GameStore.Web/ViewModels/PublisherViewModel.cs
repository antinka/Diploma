using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 40 characters and less than 3 characters")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Home page")]
        public string HomePage { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}