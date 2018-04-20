using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Payments.ViewModels
{
    public class VisaViewModel
    {
        [Display(Name = "Card holders name")]
        public string CardHoldersName { get; set; }

        [Display(Name = "Card number")]
        public int CardNumber { get; set; }

        [Display(Name = "Date of expity")]
        public DateTime DateOfExpity { get; set; }

        [Display(Name = "Card verification value")]
        public int CardVerificationValue { get; set; }

    }
}