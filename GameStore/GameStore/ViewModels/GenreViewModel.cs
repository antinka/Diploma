using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.App_LocalResources;

namespace GameStore.ViewModels
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        [Display(Name = "ParentGenreName", ResourceType = typeof(GlobalRes))]
        public string ParentGenreName { get; set; }

        [MaxLength(450)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        public ICollection<GameViewModel> Games { get; set; }

        public SelectList GenreList { get; set; }
    }
}
