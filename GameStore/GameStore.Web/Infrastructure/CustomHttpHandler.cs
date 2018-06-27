using System.Threading.Tasks;
using System.Web;

namespace GameStore.Web.Infrastructure
{
    public class CustomHttpHandler : HttpTaskAsyncHandler
    {
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            var image = context.Request.Files[0];
            var pictureName = image.FileName;
            await Task.Run(() => image.SaveAs(context.Server.MapPath($"~/Content/Images/Games/{pictureName}")));

            context.Response.Write("1231231");
        }
    }
}