using Moq;
using System.Web;
using System.Web.Routing;
using Xunit;

namespace GameStore.Tests.Routes
{
    public class RoutesTests
    {
        public RoutesTests()
        {
            RouteTable.Routes.Clear();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        [Fact]
        public void RouteTest_AddNewGame_CreateGame()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/new");
            var routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.Equal("Game", routeData.Values["controller"]);
            Assert.Equal("New", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_GetAllGames_GetAllGames()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games");
            var routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.Equal("Game", routeData.Values["controller"]);
            Assert.Equal("GetAllGames", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_GameGame_GameId()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/Key");
            var routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("GetGame", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_UpdateGame_GameId()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/update/id");
            var routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Update", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_RemoveGame_GameId()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/remove");
            var routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Remove", routeData.Values["action"]);
        }
    }
}
