using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Payments.ViewModels
{
    public class BoxViewModel
    {
        [Display(Name = "User Id")]
        public Guid UserId { get; set; }

        [Display(Name = "Order Id")]
        public Guid OrderId { get; set; }

        public decimal Cost { get; set; }

    }
}