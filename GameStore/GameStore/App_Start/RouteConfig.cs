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
                name: "games",
                url: "games",
                defaults: new { controller = "Game", action = "GetAllGames" }
            );

            routes.MapRoute(
                name: "createGame",
                url: "game/new",
                defaults: new { controller = "Game", action = "New" }
            );

            routes.MapRoute(
                name: "GameGames",
                url: "Game/Games",
                defaults: new { controller = "Game", action = "Games" }
            );

            routes.MapRoute(
                name: "filterGame",
                url: "Game/GamesFilters",
                defaults: new { controller = "Game", action = "GamesFilters" }
            );

            routes.MapRoute(
                name: "GameFilteredGames",
                url: "Game/FilteredGames",
                defaults: new { controller = "Game", action = "FilteredGames" }
            );

            routes.MapRoute(
                name: "getGame",
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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "FilteredGames", id = UrlParameter.Optional }
            );
        }
    }
}
