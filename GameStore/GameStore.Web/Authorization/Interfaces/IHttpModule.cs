using System.Web;

namespace GameStore.Web.Authorization.Interfaces
{
    public interface IHttpModule
    {
        void Dispose();

        void Init(HttpApplication context);
    }
}
