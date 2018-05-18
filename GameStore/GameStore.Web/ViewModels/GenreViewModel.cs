using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        [Display(Name = "Parent genre name")]
        public string ParentGenreName { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 200 characters and less than 3 characters")]
        [Required]
        public string Name { get; set; }

        public ICollection<GameViewModel> Games { get; set; }

        public SelectList GenreList { get; set; }
    }
}
