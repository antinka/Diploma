using System;
using System.Web.Mvc;
using GameStore.ViewModels;

namespace GameStore.Payments
{
    public interface IPayment
    {
         ActionResult Pay(OrderViewModel order, Func<string, object, ViewResult> viewResult);
    }
}
