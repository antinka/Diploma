using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}