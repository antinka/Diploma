using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Price", ResourceType = typeof(GlobalRes))]
        public decimal Price { get; set; }

        public Guid GameId { get; set; }

        public FilterGameViewModel FilterGame { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(GlobalRes))]
        public short Quantity { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(GlobalRes))]
        public float Discount { get; set; }

        public Guid OrderId { get; set; }

        public OrderViewModel Order { get; set; }
    }
}