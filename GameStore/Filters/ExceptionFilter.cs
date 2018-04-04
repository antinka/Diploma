using log4net;
using System.Web.Mvc;

namespace GameStore.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog _log = DependencyResolver.Current.GetService<ILog>();

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                _log.Info($"Exception: {filterContext.Exception.Message} source { filterContext.Exception.Source} StackTrace: { filterContext.Exception.StackTrace}");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
