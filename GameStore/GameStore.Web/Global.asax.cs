using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web.Infrastructure;

namespace GameStore.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        { 
            AutofacConfig.Setup();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
