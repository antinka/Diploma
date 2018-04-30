using System;

namespace GameStore.BLL.DTO
{
    public class OrderDetailDTO
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Guid GameId { get; set; }

        public GameDTO Game { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public Guid OrderId { get; set; }

        public OrderDTO Order { get; set; }
    }
}
