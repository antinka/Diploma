using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GameStore.Payments.Enums;
using GameStore.ViewModels;

namespace GameStore.Payments
{
    public class Payment
    {
        private readonly IPayment _payment;
        private static readonly Dictionary<PaymentTypes, IPayment> Dictionary;

        static Payment()
        {
            Dictionary = new Dictionary<PaymentTypes, IPayment>()
            {
                {PaymentTypes.Bank, new Bank() },
                {PaymentTypes.Box, new Box() },
                {PaymentTypes.Visa, new Visa() }
            };
        }

        public Payment(PaymentTypes payment)
        {
            _payment = Dictionary[payment];
        }

        public ActionResult Pay(OrderViewModel order, Func<string, object, ViewResult> viewResult)
        {
            return _payment.Pay(order, viewResult);
        }
    }
}