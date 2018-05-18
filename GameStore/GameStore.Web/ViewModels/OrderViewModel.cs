using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime? Date { get; set; }

        public Decimal Cost { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}