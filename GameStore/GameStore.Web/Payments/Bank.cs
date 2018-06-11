using System.IO;
using System.Web.Mvc;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;
using Rotativa;

namespace GameStore.Web.Payments
{
    public class Bank : IPayment
    {
        public PaymentTypes Name => PaymentTypes.Bank;

        public ActionResult Pay(OrderPayment order)
        {
            return new ViewAsPdf(order)
            {
                ViewName = "~/Views/Payments/Bank.cshtml",
            };
        }
    }
}