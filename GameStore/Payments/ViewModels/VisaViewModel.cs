using System;

namespace GameStore.Payments.ViewModels
{
    public class VisaViewModel
    {
        public string CardHoldersName { get; set; }

        public int CardNumber { get; set; }

        public DateTime DateOfExpity { get; set; }

        public int CardVerificationValue { get; set; }
    }
}