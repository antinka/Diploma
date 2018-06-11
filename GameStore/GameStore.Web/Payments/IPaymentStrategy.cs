using System.Web.Mvc;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
{
    public interface IPaymentStrategy
    {
        ActionResult GetPaymentStrategy(PaymentTypes paymentType, OrderPayment orderPayment);
    }
}
