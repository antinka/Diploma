using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.Payments.ViewModels
{
    public class BoxViewModel
    {
        [Display(Name = "UserId", ResourceType = typeof(GlobalRes))]
        public Guid UserId { get; set; }

        [Display(Name = "OrderId", ResourceType = typeof(GlobalRes))]
        public Guid OrderId { get; set; }

        [Display(Name = "Cost", ResourceType = typeof(GlobalRes))]
        public decimal Cost { get; set; }
    }
}