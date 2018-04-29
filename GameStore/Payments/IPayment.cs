using GameStore.ViewModels;
using System.Web.Mvc;

namespace GameStore.Payments
{
    public interface IPayment
    {
         ActionResult Pay(OrderPayment order);
    }
}
