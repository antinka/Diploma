using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Infrastructure;

namespace GameStore
{
    public class MvcApplication : System.Web.HttpApplication
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
