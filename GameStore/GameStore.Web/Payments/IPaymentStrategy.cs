using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;
using System.Web.Mvc;

namespace GameStore.Web.Payments
{
    public interface IPaymentStrategy
    {
        ActionResult GetPaymentStrategy(PaymentTypes paymentType, OrderPayment orderPayment);
    }
}
