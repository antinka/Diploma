using System.Web.Mvc;
using log4net;

namespace GameStore.Web.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILog _log;

        public ExceptionFilter(ILog log)
        {
            _log = log;
        }

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
