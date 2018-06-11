using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.Payments.ViewModels
{
    public class VisaViewModel
    {
        [Display(Name = "CardHoldersName", ResourceType = typeof(GlobalRes))]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3,100}", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "CardHoldersNameExpression")]
        public string CardHoldersName { get; set; }

        [Display(Name = "CardNumber", ResourceType = typeof(GlobalRes))]
        [Required]
        [RegularExpression(@"^[0-9]{3,100}", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "CardNumberExpression")]
        public int CardNumber { get; set; }

        [Display(Name = "DateOfExpity", ResourceType = typeof(GlobalRes))]
        [Required]
        public DateTime DateOfExpity { get; set; }

        [Display(Name = "CardVerificationValue", ResourceType = typeof(GlobalRes))]
        [Required]
        [RegularExpression(@"^[0-9]{4,6}", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "CardVerificationExpression")]
        public int CardVerificationValue { get; set; }

        public Guid OrderId { get; set; }
    }
}