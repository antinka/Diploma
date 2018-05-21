using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
{
    public class PaymentStrategy
    {
        private readonly IEnumerable<IPayment> _payments;

        public PaymentStrategy(IEnumerable<IPayment> payment)
        {
            _payments = payment;
        }

        public ActionResult GetPaymentStrategy(PaymentTypes paymentType, OrderPayment orderPayment)
        {
            return _payments.FirstOrDefault(x => x.Name == paymentType)?.Pay(orderPayment);
        }
    }
}