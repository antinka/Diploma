using System.IO;
using System.Web.Mvc;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Payments
{
    public class Bank : IPayment
    {
        public ActionResult Pay(OrderPayment order)
        {
			//todo Stream implements IDisposable interface, so it must be disposed
            Stream stream = new MemoryStream();

            return new FileStreamResult(stream, "application/txt");
        }
    }
}