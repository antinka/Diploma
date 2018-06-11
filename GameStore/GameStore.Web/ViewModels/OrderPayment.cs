using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class OrderPayment
    {
        [Display(Name = "OrderId", ResourceType = typeof(GlobalRes))]
        public Guid Id { get; set; }

        [Display(Name = "UserId", ResourceType = typeof(GlobalRes))]
        public Guid UserId { get; set; }

        [Display(Name = "Cost", ResourceType = typeof(GlobalRes))]
        public decimal Cost { get; set; }
    }
}