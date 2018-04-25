using GameStore.ViewModels;
using System.IO;
using System.Web.Mvc;

namespace GameStore.Payments
{
    public class Bank : IPayment
    {
        public ActionResult Pay(OrderPayment order)
        {
            Stream stream = new MemoryStream();

            return new FileStreamResult(stream, "application/txt");
        }
    }
}