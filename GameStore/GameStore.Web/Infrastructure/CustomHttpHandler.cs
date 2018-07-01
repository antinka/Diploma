using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;

namespace GameStore.Web.Infrastructure
{
    public class CustomHttpHandler : HttpTaskAsyncHandler
    {
        private readonly IGameService _gameService;

        public CustomHttpHandler()
        {
            _gameService = DependencyResolver.Current.GetService<IGameService>();
        }

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            string gameKey = context.Request.Url.Segments.Last();
            var image = context.Request.Files[0];
            var pictureName = image.FileName;
            var imageMimeType = image.ContentType;
            await Task.Run(() => image.SaveAs(context.Server.MapPath($"~/Content/Images/Games/{pictureName}")));
            _gameService.UpdateImage(gameKey, pictureName, imageMimeType);

            context.Response.Write("1231231");
        }
    }
}