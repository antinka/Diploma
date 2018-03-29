using GameStore.Filters;
using System.Web.Mvc;

namespace GameStore.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackRequestIP());
            filters.Add(new ExceptionFilter());
        }
    }
}