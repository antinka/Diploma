using System.Web.Mvc;
using GameStore.Web.Payments.Enums;
using GameStore.Web.Payments.ViewModels;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
{
    public class Visa : IPayment
    {
        public PaymentTypes Name => PaymentTypes.Visa;

        public ActionResult Pay(OrderPayment order)
        {
            return new ViewResult()
            {
                ViewName = "~/Views/Payments/Visa.cshtml",

                ViewData = new ViewDataDictionary(new VisaViewModel { OrderId = order.Id })
            };
        }
    }
}