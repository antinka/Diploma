using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "game",
                url: "game/{Key}",
                defaults: new { controller = "Game", action = "GetGame", Key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "editGame",
                url: "games/update/{id}",
                defaults: new { controller = "Game", action = "Update", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "gamesRemove",
                url: "games/remove/{id}",
                defaults: new { controller = "Game", action = "Remove", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "commentForGame",
                url: "game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "CommentToGame", gamekey = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "getAllComment",
                url: "game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "GetAllCommentToGame", gamekey = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Download",
                url: "game/{gamekey}/download",
                defaults: new { controller = "Game", action = "Download", gamekey = UrlParameter.Optional }
            );

            routes.MapRoute(
                  name: "createGame",
                  url: "games/new/{id}",
                  defaults: new { controller = "Game", action = "New", id = UrlParameter.Optional }
              );

            routes.MapRoute(
               name: "games",
               url: "games",
               defaults: new { controller = "Game", action = "GetAllGames" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
