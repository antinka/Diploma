using log4net;
using System.Web.Mvc;

namespace GameStore.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILog _log = LogManager.GetLogger("LOGGER");
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                _log.Info("Exception: "+filterContext.Exception.Message+" " + "source "+ filterContext.Exception.Source+ " StackTrace: "+ filterContext.Exception.StackTrace);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
