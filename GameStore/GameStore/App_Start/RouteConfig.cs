using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "games",
                url: "{lang}/games",
                defaults: new { controller = "Game", action = "GetAllGames", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "BasketInfo",
                url: "{lang}/basket/{id}",
                defaults: new { controller = "Order", action = "BasketInfo", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "createGame",
                url: "{lang}/games/new",
                defaults: new { controller = "Game", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "GameFilteredGames",
                url: "{lang}/Game/FilteredGames",
                defaults: new { controller = "Game", action = "FilteredGames", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "getGame",
                url: "{lang}/game/{Key}",
                defaults: new { controller = "Game", action = "GetGame", Key = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "editGame",
                url: "{lang}/games/update/{id}",
                defaults: new { controller = "Game", action = "Update", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "gamesRemove",
                url: "{lang}/games/remove/{id}",
                defaults: new { controller = "Game", action = "Remove", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "commentForGame",
                url: "{lang}/game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "CommentToGame", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "getAllComment",
                url: "{lang}/game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "GetAllCommentToGame", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "Download",
                url: "{lang}/game/{gamekey}/download",
                defaults: new { controller = "Game", action = "Download", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                       name: "FilterOrders",
                       url: "{lang}/orders",
                       defaults: new { controller = "Order", action = "FilterOrders", lang = "en" },
                       constraints: new { lang = @"ru|en" }
                   );

            routes.MapRoute(
                name: "publishers",
                url: "{lang}/publishers",
                defaults: new { controller = "Publisher", action = "GetAll", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "getPublisher",
                url: "{lang}/publisher/{Name}",
                defaults: new { controller = "Publisher", action = "Get", Name = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                           name: "publishersNew",
                           url: "{lang}/publishers/new",
                           defaults: new { controller = "Publisher", action = "New", lang = "en" },
                           constraints: new { lang = @"ru|en" }
                       );

            routes.MapRoute(
                name: "editPublisher",
                url: "{lang}/publishers/update/{Name}",
                defaults: new { controller = "Publisher", action = "Update", Name = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "platformTypes",
                url: "{lang}/platformTypes",
                defaults: new { controller = "PlatformType", action = "GetAll", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "getPlatformType",
                url: "{lang}/platformType/{Name}",
                defaults: new { controller = "PlatformType", action = "Get", Name = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "newPlatformType",
                url: "{lang}/platformTypes/new",
                defaults: new { controller = "PlatformType", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "editPlatformType",
                url: "{lang}/platformType/update/{Name}",
                defaults: new { controller = "PlatformType", action = "Update", Name = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "genres",
                url: "{lang}/genres",
                defaults: new { controller = "Genre", action = "GetAll", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "getGenre",
                url: "{lang}/genre/{Name}",
                defaults: new { controller = "Genre", action = "Get", Name = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "newGenre",
                url: "{lang}/genre/new",
                defaults: new { controller = "Genre", action = "New", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "editGenre",
                url: "{lang}/genre/update/{Name}",
                defaults: new { controller = "Genre", action = "Update", Name = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "FilteredGames", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );
        }
    }
}
