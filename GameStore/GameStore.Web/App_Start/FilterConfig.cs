using System.Web.Mvc;
using GameStore.Web.Filters;

namespace GameStore.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackRequestIp());
            filters.Add(new ExceptionFilter());
        }
    }
}