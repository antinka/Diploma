using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web.Authorization;
using GameStore.Web.Authorization.Implementation;
using GameStore.Web.Authorization.Interfaces;

namespace GameStore.Web.Controllers
{
    public class BaseController : Controller
    {
        public string CurrentLangCode { get; protected set; }

        public IAuthentication Authentication { get; }

        public BaseController(IAuthentication authentication)
        {
            Authentication = authentication;
        }

        public User CurrentUser
        {
            get { return ((UserIdentity)Authentication.CurrentUser.Identity).User; }
        }
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