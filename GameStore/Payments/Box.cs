using GameStore.ViewModels;
using System;
using System.Web.Mvc;

namespace GameStore.Payments
{
    public class Box : IPayment
    {
        public ActionResult Pay(OrderViewModel order, Func<string, object, ViewResult> viewResult)
        {
            throw new NotImplementedException();
        }
    }
}