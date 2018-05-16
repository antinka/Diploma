using System.Web.Mvc;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
{
    public interface IPayment
    {
         ActionResult Pay(OrderPayment order);
    }
}
