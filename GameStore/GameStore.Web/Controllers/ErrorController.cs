using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}