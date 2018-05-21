using System.Web.Mvc;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
{
    public interface IPayment
    {
        PaymentTypes Name { get; }

        ActionResult Pay(OrderPayment order);
    }
}
