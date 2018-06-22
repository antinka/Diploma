using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;

namespace GameStore.Web.Infrastructure
{
    public class CustomHttpHandler : HttpTaskAsyncHandler
    {
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            var key = context.Request.Url.Segments.Last();
            var gameService = DependencyResolver.Current.GetService<IGameService>();
            var game = await Task.Run(() => gameService.GetByKey(key));
            var imagePath = context.Server.MapPath($"~/Content/Images/Games/{game.ImageName}");
            var imageByteData = File.ReadAllBytes(imagePath);

            context.Response.BinaryWrite(imageByteData);
            context.Response.ContentType = game.ImageMimeType;
        }
    }
}