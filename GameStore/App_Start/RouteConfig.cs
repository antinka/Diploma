using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                  name: "createGame",
                  url: "games/new/{id}",
                  defaults: new { controller = "Game", action = "New", id = UrlParameter.Optional }
              );

            routes.MapRoute(
                 name: "editGame",
                 url: "games/update/{id}",
                 defaults: new { controller = "Game", action = "Update", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "game",
                url: "game/{Key}",
                defaults: new { controller = "Game", action = "GetGameById", Key = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "games",
               url: "games",
               defaults: new { controller = "Game", action = "GetAllGames" }
           );

            routes.MapRoute(
             name: "gamesRemove",
             url: "games/remove/{id}",
             defaults: new { controller = "Game", action = "GetAllGames", id = UrlParameter.Optional }
         );

            routes.MapRoute(
               name: "commentForGame",
               url: "game/{gamekey}/newcomment",
               defaults: new { controller = "Comment", action = "CommentToGame", gamekey = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "commentForComment",
              url: "game/{gamekey}/newcomment",
              defaults: new { controller = "Comment", action = "CommentToComment", gamekey = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "getAllComment",
             url: "game/{gamekey}/newcomment",
             defaults: new { controller = "Comment", action = "CommentToComment", gamekey = UrlParameter.Optional }
         );

            routes.MapRoute(
               name: "Download",
               url: "game/{gamekey}/newcomment",
               defaults: new { controller = "Comment", action = "Download", gamekey = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

         
        }
    }
}
