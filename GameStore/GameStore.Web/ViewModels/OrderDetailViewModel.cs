using System;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Guid GameId { get; set; }

        public GameViewModel Game { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public Guid OrderId { get; set; }

        public OrderViewModel Order { get; set; }
    }
}