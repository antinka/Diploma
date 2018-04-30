using GameStore.Filters;
using System.Web.Mvc;

namespace GameStore
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