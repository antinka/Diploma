using System;

namespace GameStore.Payments.ViewModels
{
    public class BoxViewModel
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public decimal Cost { get; set; }
    }
}