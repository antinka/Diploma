using GameStore.Filters;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class PlatformTypeController : Controller
    {
    }
}