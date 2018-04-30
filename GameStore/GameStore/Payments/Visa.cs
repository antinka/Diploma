using GameStore.ViewModels;
using System;
using System.Web.Mvc;

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