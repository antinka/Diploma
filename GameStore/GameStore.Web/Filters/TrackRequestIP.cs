using System.Web.Mvc;
using log4net;

namespace GameStore.Web.Filters
{
    public class TrackRequestIp : FilterAttribute, IActionFilter
    {
        private readonly ILog _log;

        public TrackRequestIp(ILog log)
        {
            _log = log;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userIp = filterContext.HttpContext.Request.UserHostAddress;
            if (filterContext.HttpContext.Request.Url != null)
            {
                _log.Info("Path: " + filterContext.HttpContext.Request.Url.PathAndQuery + " IP: " + userIp + " " +
                         "Attempted");
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userIp = filterContext.HttpContext.Request.UserHostAddress;
            if (filterContext.HttpContext.Request.Url != null)
            {
                _log.Info("Path: " + filterContext.HttpContext.Request.Url.PathAndQuery + " IP: " + userIp + " " +
                         "Completed");
            }
        }
    }
}
