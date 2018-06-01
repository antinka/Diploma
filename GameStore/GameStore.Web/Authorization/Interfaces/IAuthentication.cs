using System.Web;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Authorization.Interfaces
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }

        UserViewModel Login(string login, string password, bool isPersistent);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
