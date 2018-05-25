using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web.Controllers
{
    public class BaseController : Controller
    {
        public string CurrentLangCode { get; protected set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null &&
                requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string ?? "ru";
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurrentLangCode);
            }
            base.Initialize(requestContext);
        }
    }
}