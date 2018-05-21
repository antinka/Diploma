using System;

namespace GameStore.Web.ViewModels
{
    public class OrderPayment
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Decimal Cost { get; set; }
    }
}