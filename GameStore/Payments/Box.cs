using GameStore.ViewModels;
using System.Web.Mvc;
using GameStore.Payments.ViewModels;

namespace GameStore.Payments
{
    public class Box : IPayment
    {
        public ActionResult Pay(OrderPayment order)
        {
            var box = new BoxViewModel()
            {
                Cost = order.Cost,
                OrderId = order.Id,
                UserId = order.UserId
            };

            return new ViewResult()
            {
                ViewName = "~/Views/Payments/IBox.cshtml",
                ViewData = new ViewDataDictionary(box)
            };
        }
    }
}