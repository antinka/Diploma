using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Controllers;
using GameStore.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class GameControllerTest :IDisposable
    {
        private static readonly Mock<IGameService> GameRepo = new Mock<IGameService>();
        private readonly List<GameDTO> games = new List<GameDTO>();
        public Guid Id = Guid.NewGuid();
        private bool _boolDelete, _boolUpdate, _boolGet;
        private readonly GameController _gameController = new GameController(GameRepo.Object);

        public GameControllerTest()
        {
            AutoMapper.Mapper.Reset();
            GameRepo.Setup(x => x.AddNewGame(It.IsAny<GameDTO>())).Callback(() => games.Add(It.IsAny<GameDTO>()));
            GameRepo.Setup(x => x.EditGame(It.IsAny<GameDTO>())).Callback(() => _boolUpdate = true);
            GameRepo.Setup(x => x.DeleteGame(It.IsAny<Guid>())).Callback(() => _boolDelete = true);
            GameRepo.Setup(x => x.GetGame(It.IsAny<Guid>())).Callback(() => _boolGet = true);
        }

        [Fact]
        public void NewGame_AddNewGame_NewGameInList()
        {
            var game = new GameViewModel();

            var result = _gameController.New(game);
            var countNewGames = games.Count();

            Assert.Equal(1, countNewGames);
        }

        [Fact]
        public void UpdateGame_GameDTO_boolUpdateTrue()
        {
            var game = new GameViewModel();

            _gameController.Update(game);

            Xunit.Assert.True(_boolUpdate);
        }

        [Fact]
        public void DeleteGame_Id_boolDeleteTrue()
        {
            _gameController.Remove(Id);

            Xunit.Assert.True(_boolDelete);
        }

        [Fact]
        public void GetGame_Id_boolGetTrue()
        {
            _gameController.GetGameById(Id);

            Xunit.Assert.True(_boolGet);
        }

        public void Dispose()
        {
            ((IDisposable)_gameController).Dispose();
        }
    }
}
