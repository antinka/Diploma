using System.Web.Mvc;
using GameStore.Filters;

namespace GameStore.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class GenreController : Controller
    {
    }
}