using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "NameExpression3_40")]
        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        [Display(Name = "DescriptionEn", ResourceType = typeof(GlobalRes))]
        public string DescriptionEn { get; set; }

        [Display(Name = "DescriptionRu", ResourceType = typeof(GlobalRes))]
        public string DescriptionRu { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "HomePage", ResourceType = typeof(GlobalRes))]
        public string HomePage { get; set; }

        public ICollection<FilterGameViewModel> Games { get; set; }
    }
}