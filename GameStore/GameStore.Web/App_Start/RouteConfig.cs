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
                url: "{lang}/order",
                defaults: new { controller = "Order", action = "Order", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "games",
                url: "{lang}/games",
                defaults: new { controller = "Game", action = "FilteredGames", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "createGame",
                url: "{lang}/games/new",
                defaults: new { controller = "Game", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "GameGames",
                url: "{lang}/Game/Games",
                defaults: new { controller = "Game", action = "Games", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "getGame",
                url: "{lang}/game/{gamekey}",
                defaults: new { controller = "Game", action = "GetGame", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "editGame",
                url: "{lang}/games/update/{gamekey}",
                defaults: new { controller = "Game", action = "Update", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "gamesRemove",
                url: "{lang}/games/remove",
                defaults: new { controller = "Game", action = "Remove", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "PublisherRemove",
                url: "{lang}/publishers/remove",
                defaults: new { controller = "Publisher", action = "Remove", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "GenreRemove",
                url: "{lang}/genres/remove",
                defaults: new { controller = "Genre", action = "Remove", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "PlatformTypeRemove",
                url: "{lang}/platformTypes/remove",
                defaults: new { controller = "PlatformType", action = "Remove", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "buyGame",
                url: "{lang}/game/{gamekey}/buy",
                defaults: new { controller = "Order", action = "AddGameToOrder", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "commentForGame",
                url: "{lang}/game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "CommentToGame", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "getAllComment",
                url: "{lang}/game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "GetAllCommentToGame", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "Download",
                url: "{lang}/game/{gamekey}/download",
                defaults: new { controller = "Game", action = "Download", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
               name: "publishers",
               url: "{lang}/publishers",
               defaults: new { controller = "Publisher", action = "GetAll", lang = "en" },
               constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "publishersNew",
                url: "{lang}/publishers/new",
                defaults: new { controller = "Publisher", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "editPublisher",
                url: "{lang}/publishers/update/{companyName}",
                defaults: new { controller = "Publisher", action = "Update", companyName = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "getPublisher",
                url: "{lang}/publisher/{companyName}",
                defaults: new { controller = "Publisher", action = "Get", companyName = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "platformTypes",
                url: "{lang}/platformTypes",
                defaults: new { controller = "PlatformType", action = "GetAll", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "newPlatformType",
                url: "{lang}/platformTypes/new",
                defaults: new { controller = "PlatformType", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "editPlatformType",
                url: "{lang}/platformType/update/{platformTypeName}",
                defaults: new { controller = "PlatformType", action = "Update", platformTypeName = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "getPlatformType",
                url: "{lang}/platformType/{platformTypeName}",
                defaults: new { controller = "PlatformType", action = "Get", platformTypeName = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "genres",
                url: "{lang}/genres",
                defaults: new { controller = "Genre", action = "GetAll", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "newGenre",
                url: "{lang}/genre/new",
                defaults: new { controller = "Genre", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "editGenre",
                url: "{lang}/genre/update/{genreName}",
                defaults: new { controller = "Genre", action = "Update", genreName = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "getGenre",
                url: "{lang}/genre/{genreName}",
                defaults: new { controller = "Genre", action = "Get", genreName = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "BasketInfo",
                url: "{lang}/basket",
                defaults: new { controller = "Order", action = "BasketInfo", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "OrdersHistory",
                url: "{lang}/Orders/history",
                defaults: new { controller = "Order", action = "FilterOrders", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "FilteredGames", id = UrlParameter.Optional, lang = "en" });
        }
    }
}
