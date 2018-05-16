using System.IO;
using System.Web.Mvc;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
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