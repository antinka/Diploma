using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentGenreId { get; set; }

        [Display(Name = "ParentGenreName", ResourceType = typeof(GlobalRes))]
        public string ParentGenreName { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "NameExpression3_200")]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [Display(Name = "NameEn", ResourceType = typeof(GlobalRes))]
        public string NameEn { get; set; }

        [Display(Name = "NameRu", ResourceType = typeof(GlobalRes))]
        public string NameRu { get; set; }

        public ICollection<FilterGameViewModel> Games { get; set; }

        public SelectList GenreList { get; set; }
    }
}