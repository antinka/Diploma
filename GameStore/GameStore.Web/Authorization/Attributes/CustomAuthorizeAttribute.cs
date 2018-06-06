using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web.Authorization.Implementation;

namespace GameStore.Web.Authorization.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public UserRole UserRole { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ((UserProvider)httpContext.User).IsInRole(UserRole.ToString());
        }
    }
}