using System.Runtime.InteropServices;

namespace GameStore.Web.Authorization.Interfaces
{
    [ComVisible(true)]
    public interface IPrincipal
    {
        IIdentity Identity { get; }

        bool IsInRole(string role);
    }
}
