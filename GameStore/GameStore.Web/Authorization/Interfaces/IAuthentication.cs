using System.Web;

namespace GameStore.Web.Authorization.Interfaces
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }

        //User Login(string login, string password, bool isPersistent);

        //User Login(string login);

        void LogOut();

        IPrincipal CurrentUser { get; }

    }
}
