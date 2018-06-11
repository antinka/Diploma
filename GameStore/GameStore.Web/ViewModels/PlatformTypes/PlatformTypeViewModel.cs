using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class PlatformTypeViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [StringLength(200, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "NameExpression3_200")]
        [MaxLength(450)]
        [Display(Name = "NameEn", ResourceType = typeof(GlobalRes))]
        public string NameEn { get; set; }

        [Display(Name = "NameRu", ResourceType = typeof(GlobalRes))]
        public string NameRu { get; set; }

        public ICollection<FilterGameViewModel> Games { get; set; }
    }
}
