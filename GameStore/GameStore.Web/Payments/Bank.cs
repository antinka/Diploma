using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModels;
using System.IO;
using System.Web.Mvc;

namespace GameStore.Web.Payments
{
    public class Bank : IPayment
    {
        public PaymentTypes Name => PaymentTypes.Bank;

        public ActionResult Pay(OrderPayment order)
        {
            using (Stream stream = new MemoryStream())
            {
                return new FileStreamResult(stream, "application/txt");
            }
        }
    }
}