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

        [Display(Name = "Ship Via")]
        public int? ShipVia { get; set; }

        [Display(Name = "Freight")]
        public decimal? Freight { get; set; }

        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }

        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }

        [Display(Name = "Ship City")]
        public string ShipCity { get; set; }

        [Display(Name = "Ship Region")]
        public string ShipRegion { get; set; }

        [Display(Name = "Ship Postal Code")]
        public string ShipPostalCode { get; set; }

        [Display(Name = "Ship Country")]
        public string ShipCountry { get; set; }

        public SelectList ShipperList { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}