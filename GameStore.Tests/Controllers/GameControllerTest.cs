using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class GameControllerTest
    {
        private readonly Mock<IGameService> _gameService;
        private readonly GameController _sut;
        private readonly List<GameDTO> _fakeGames;
        private readonly Guid _gamekey;

        public GameControllerTest()
        {
            _gamekey = Guid.NewGuid();
            var mapper = new Mock<IMapper>();
            _gameService = new Mock<IGameService>();
            _sut = new GameController(_gameService.Object, mapper.Object);

            _fakeGames = new List<GameDTO>
            {
                new GameDTO()
                {
                    Id = Guid.NewGuid(),
                    Key = "1"
                },
                new GameDTO()
                {
                Id = Guid.NewGuid(),
                Key = "2"
                }
            };
        }

        [Fact]
        public void NewGame_ValidGame_HttpStatusCodeOK()
        {
            _gameService.Setup(service => service.AddNew(It.IsAny<GameDTO>()));

            var httpStatusCodeResult = _sut.New(It.IsAny<GameViewModel>()) as HttpStatusCodeResult;

            Assert.Equal(200, httpStatusCodeResult.StatusCode);
        }

        [Fact]
        public void UpdateGame_Game_HttpStatusCodeOK()
        {
            _gameService.Setup(service => service.Update(It.IsAny<GameDTO>())).Verifiable();

            var httpStatusCodeResult = _sut.Update(It.IsAny<GameViewModel>()) as HttpStatusCodeResult;

            Assert.Equal(200, httpStatusCodeResult.StatusCode);
        }

        [Fact]
        public void RemoveGame_GameKey_HttpStatusCodeOK()
        {
            _gameService.Setup(service => service.Delete(_gamekey)).Verifiable();

            var httpStatusCodeResult = _sut.Remove(_gamekey) as HttpStatusCodeResult;

            Assert.Equal(200, httpStatusCodeResult.StatusCode);
        }

        [Fact]
        public void GetAllGames_ReturnedGames()
        {
            _gameService.Setup(service => service.GetAll()).Returns(_fakeGames);

            var game = _sut.GetAllGames() as JsonResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(game.Data);

            Assert.Equal(_fakeGames.Count, data.Count);
        }
    }
}
