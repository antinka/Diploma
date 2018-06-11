using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime? Date { get; set; }

        public decimal Cost { get; set; }

        public string ShipperId { get; set; }

        public int? ShipVia { get; set; }

        public string ShipViaName { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
