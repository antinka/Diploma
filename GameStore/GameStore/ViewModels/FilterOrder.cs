using System;
using System.Collections.Generic;

namespace GameStore.ViewModels
{
    public class FilterOrder
    {
        public DateTime? DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; }

        public IEnumerable<OrderViewModel> OrdersViewModel { get; set; }
    }
}