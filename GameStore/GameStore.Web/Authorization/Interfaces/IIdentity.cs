using System.Runtime.InteropServices;

namespace GameStore.Web.Authorization.Interfaces
{
    [ComVisible(true)]
    public interface IIdentity
    {
        string AuthenticationType { get; }

        bool IsAuthenticated { get; }

        string Name { get; }
    }
}
