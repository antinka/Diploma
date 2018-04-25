﻿using log4net;
using System.Web.Mvc;

namespace GameStore.Filters
{
    public class TrackRequestIp : FilterAttribute, IActionFilter
    {
        private static readonly ILog _log = DependencyResolver.Current.GetService<ILog>();

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userIp = filterContext.HttpContext.Request.UserHostAddress;
            if (filterContext.HttpContext.Request.Url != null)
                _log.Info("Path: " + filterContext.HttpContext.Request.Url.PathAndQuery + " IP: " + userIp + " " +
                          "Attempted");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userIp = filterContext.HttpContext.Request.UserHostAddress;
            if (filterContext.HttpContext.Request.Url != null)
                _log.Info("Path: " + filterContext.HttpContext.Request.Url.PathAndQuery + " IP: " + userIp + " " +
                          "Completed");
        }
    }
}
