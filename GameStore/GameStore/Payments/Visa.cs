using GameStore.ViewModels;
using System.Web.Mvc;
using GameStore.Payments.ViewModels;

namespace GameStore.Payments
{
    public class Visa : IPayment
    {
        public ActionResult Pay(OrderPayment order)
        {
            return new ViewResult()
            {
                ViewName = "~/Views/Payments/Visa.cshtml",
                ViewData = new ViewDataDictionary(),
            };
        }
    }
}