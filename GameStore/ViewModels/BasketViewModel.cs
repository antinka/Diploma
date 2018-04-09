using System;

namespace GameStore.ViewModels
{
    public class BasketViewModel
    {
        public Guid GameId { get; set; }

        public Guid UserId { get; set; }

        public short Quantity { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }
    }
}