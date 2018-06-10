using System.Security.Principal;
using System.Web;

namespace GameStore.Web.Authorization.Interfaces
{
    public interface IAuthentication
    {
        IPrincipal CurrentUser { get; }

        HttpContext HttpContext { get; set; }

        bool Login(string login, string password, bool isPersistent);

        void LogOut();
    }
}
