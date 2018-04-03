using GameStore.Filters;
using System.Web.Mvc;
using GameStore.Infastracture;
using log4net;

namespace GameStore
{
    public class FilterConfig
    {
        private static readonly ILog Log= DependencyResolver.Current.GetService<ILog>();

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackRequestIp(Log));
            filters.Add(new ExceptionFilter(Log));
        }
    }
}