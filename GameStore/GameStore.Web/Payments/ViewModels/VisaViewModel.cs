using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Payments.ViewModels
{
    public class VisaViewModel
    {
        [Display(Name = "Card holders name")]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3,100}", ErrorMessage = "Card holder's name could consist only from characters,  not less than 3")]
        public string CardHoldersName { get; set; }

        [Display(Name = "Card number")]
        [Required]
        [RegularExpression(@"^[0-9]{3,100}", ErrorMessage = "Card number could consist only from numbers, not less than 3")]
        public int CardNumber { get; set; }

        [Display(Name = "Date of expity")]
        [Required]
        public DateTime DateOfExpity { get; set; }

        [Display(Name = "Card verification value")]
        [Required]
        [RegularExpression(@"^[0-9]{4,6}", ErrorMessage = "Card verification value could consist only from numbers, not less than 4, no more than 6")]
        public int CardVerificationValue { get; set; }
    }
}