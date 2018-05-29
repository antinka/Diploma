using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class FilterOrder
    {
        [Display(Name = "DateTimeFrom", ResourceType = typeof(GlobalRes))]
        public DateTime? DateTimeFrom { get; set; }

        [Display(Name = "DateTimeTo", ResourceType = typeof(GlobalRes))]
        public DateTime? DateTimeTo { get; set; }

        public IEnumerable<OrderViewModel> OrdersViewModel { get; set; }
    }
}