using System.Web.Mvc;

namespace GameStore.Controllers
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