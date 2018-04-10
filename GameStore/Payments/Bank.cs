using System;
using System.Web.Mvc;
using GameStore.ViewModels;

namespace GameStore.Payments
{
    public class Bank : IPayment
    {
        public ActionResult Pay(OrderViewModel order, Func<string, object, ViewResult> viewResult)
        {
            throw new NotImplementedException();
        }
    }
}