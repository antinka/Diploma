using System;
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
            var gameService = DependencyResolver.Current.GetServices<IGameService>();

            string url = context.Request.Url.Segments.Last();
           // var game = await Task.Run(() => gameService.GetByKey(url.));

            //var imagePath = Server.MapPath($"~/Content/Images/Games/{game.ImageName}");

            //return File(imagePath, game.ImageMimeType);
            throw new NotImplementedException();
        }
    }
}