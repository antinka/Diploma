using System.Web.Mvc;
using GameStore.Web.Filters;
using log4net;

namespace GameStore.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}