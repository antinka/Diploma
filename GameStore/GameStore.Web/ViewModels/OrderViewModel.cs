using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "UserId", ResourceType = typeof(GlobalRes))]
        public Guid UserId { get; set; }

        [Display(Name = "IsPaid", ResourceType = typeof(GlobalRes))]
        public bool IsPaid { get; set; }

        [Display(Name = "Date", ResourceType = typeof(GlobalRes))]
        public DateTime? Date { get; set; }

        [Display(Name = "ShippedDate", ResourceType = typeof(GlobalRes))]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "Cost", ResourceType = typeof(GlobalRes))]
        public Decimal Cost { get; set; }

        [Display(Name = "Shipper", ResourceType = typeof(GlobalRes))]
        public string ShipperId { get; set; }

        [Display(Name = "ShipVia", ResourceType = typeof(GlobalRes))]
        public int? ShipVia { get; set; }

        [Display(Name = "Freight", ResourceType = typeof(GlobalRes))]
        public decimal? Freight { get; set; }

        [Display(Name = "ShipName", ResourceType = typeof(GlobalRes))]
        public string ShipName { get; set; }

        [Display(Name = "ShipAddress", ResourceType = typeof(GlobalRes))]
        public string ShipAddress { get; set; }

        [Display(Name = "ShipCity", ResourceType = typeof(GlobalRes))]
        public string ShipCity { get; set; }

        [Display(Name = "ShipRegion", ResourceType = typeof(GlobalRes))]
        public string ShipRegion { get; set; }

        [Display(Name = "ShipPostalCode", ResourceType = typeof(GlobalRes))]
        public string ShipPostalCode { get; set; }

        [Display(Name = "ShipCountry", ResourceType = typeof(GlobalRes))]
        public string ShipCountry { get; set; }

        public SelectList ShipperList { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}