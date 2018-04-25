using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public Decimal Cost { get; set; }

        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
