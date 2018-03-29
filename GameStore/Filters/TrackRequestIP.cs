using log4net;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace GameStore.Filters
{
    public class TrackRequestIP : FilterAttribute, IActionFilter
    {
        ILog log = LogManager.GetLogger("LOGGER");
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userIP = filterContext.HttpContext.Request.UserHostAddress;
            log.Info("Path: "+filterContext.HttpContext.Request.Url.PathAndQuery+" IP: "+userIP+" "+"Attempted");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string userIP = filterContext.HttpContext.Request.UserHostAddress;
            log.Info("Path: " + filterContext.HttpContext.Request.Url.PathAndQuery + " IP: " + userIP + " " + "Completed");
        }
    }
}
