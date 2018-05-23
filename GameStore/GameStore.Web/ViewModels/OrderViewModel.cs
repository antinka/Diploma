using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime? Date { get; set; }

        public Decimal Cost { get; set; }

        [Display(Name = "Shipper")]
        public string ShipperId { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        public SelectList ShipperList { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}