using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;

namespace GameStore.Web.Authorization.Interfaces
{
    [ComVisible(true)]
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }

        User Login(string login, string password, bool isPersistent);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
