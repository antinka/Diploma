using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "UserId", ResourceType = typeof(GlobalRes))]
        public Guid UserId { get; set; }

        [Display(Name = "Date", ResourceType = typeof(GlobalRes))]
        public DateTime? Date { get; set; }

        [Display(Name = "Cost", ResourceType = typeof(GlobalRes))]
        public Decimal Cost { get; set; }

        [Display(Name = "Shipper", ResourceType = typeof(GlobalRes))]
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