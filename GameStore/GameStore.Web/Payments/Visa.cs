using System.Web.Mvc;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
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