using GameStore.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
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