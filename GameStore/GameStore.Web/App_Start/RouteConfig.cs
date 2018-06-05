using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "order",
                url: "order",
                defaults: new { controller = "Order", action = "Order" }
            );

            routes.MapRoute(
                name: "games",
                url: "games",
                defaults: new { controller = "Game", action = "FilteredGames" }
            );

            routes.MapRoute(
                name: "createGame",
                url: "games/new",
                defaults: new { controller = "Game", action = "New" }
            );
            routes.MapRoute(
                name: "GameGames",
                url: "Game/Games",
                defaults: new { controller = "Game", action = "Games" }
            );

            routes.MapRoute(
                name: "getGame",
                url: "game/{gamekey}",
                defaults: new { controller = "Game", action = "GetGame", gamekey = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "editGame",
                url: "games/update/{gamekey}",
                defaults: new { controller = "Game", action = "Update", gamekey = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "gamesRemove",
                url: "games/remove",
                defaults: new { controller = "Game", action = "Remove"}
            );

            routes.MapRoute(
                name: "PublisherRemove",
                url: "publishers/remove",
                defaults: new { controller = "Publisher", action = "Remove" }
            );

            routes.MapRoute(
                name: "GenreRemove",
                url: "genres/remove",
                defaults: new { controller = "Genre", action = "Remove" }
            );

            routes.MapRoute(
                name: "PlatformTypeRemove",
                url: "platformTypes/remove",
                defaults: new { controller = "PlatformType", action = "Remove" }
            );

            routes.MapRoute(
                name: "buyGame",
                url: "game/{gamekey}/buy",
                defaults: new { controller = "Order", action = "AddGameToOrder", gamekey = UrlParameter.Optional }
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
               name: "publishers",
               url: "publishers",
               defaults: new { controller = "Publisher", action = "GetAll"}
           );

            routes.MapRoute(
                name: "publishersNew",
                url: "publishers/new",
                defaults: new { controller = "Publisher", action = "New" }
                );

            routes.MapRoute(
                name: "editPublisher",
                url: "publishers/update/{companyName}",
                defaults: new { controller = "Publisher", action = "Update", companyName = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "getPublisher",
                url: "publisher/{companyName}",
                defaults: new { controller = "Publisher", action = "Get", companyName = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "platformTypes",
                url: "platformTypes",
                defaults: new { controller = "PlatformType", action = "GetAll"}
            );

            routes.MapRoute(
                name: "newPlatformType",
                url: "platformTypes/new",
                defaults: new { controller = "PlatformType", action = "New" }
            );

            routes.MapRoute(
                name: "editPlatformType",
                url: "platformType/update/{platformTypeName}",
                defaults: new { controller = "PlatformType", action = "Update", platformTypeName = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "getPlatformType",
                url: "platformType/{platformTypeName}",
                defaults: new { controller = "PlatformType", action = "Get", platformTypeName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "genres",
                url: "genres",
                defaults: new { controller = "Genre", action = "GetAll"}
            );

            routes.MapRoute(
                name: "newGenre",
                url: "genre/new",
                defaults: new { controller = "Genre", action = "New" }
            );

            routes.MapRoute(
                name: "editGenre",
                url: "genre/update/{genreName}",
                defaults: new { controller = "Genre", action = "Update", genreName = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "getGenre",
                url: "genre/{genreName}",
                defaults: new { controller = "Genre", action = "Get", genreName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BasketInfo",
                url: "basket",
                defaults: new { controller = "Order", action = "BasketInfo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "FilteredGames", id = UrlParameter.Optional }
            );
        }
    }
}
