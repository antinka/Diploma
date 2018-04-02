using AutoMapper;
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
    public class GameControllerTest 
    {
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
        private static readonly Mock<IGameService> GameRepo = new Mock<IGameService>();
        private readonly List<GameDTO> _games = new List<GameDTO>();
        public Guid Id = Guid.NewGuid();
        private bool _isDelete, _isUpdate, _isGet;
        private readonly GameController _gameController = new GameController(GameRepo.Object, Mapper.Object);

        public GameControllerTest()
        {
            AutoMapper.Mapper.Reset();
            GameRepo.Setup(x => x.AddNewGame(It.IsAny<GameDTO>())).Callback(() => _games.Add(It.IsAny<GameDTO>()));
            GameRepo.Setup(x => x.UpdateGame(It.IsAny<GameDTO>())).Callback(() => _isUpdate = true);
            GameRepo.Setup(x => x.DeleteGame(It.IsAny<Guid>())).Callback(() => _isDelete = true);
            GameRepo.Setup(x => x.GetGame(It.IsAny<Guid>())).Callback(() => _isGet = true);
        }

        [Fact]
        public void NewGame_AddNewGame_NewGameInList()
        {
            var game = new GameViewModel();

            var result = _gameController.New(game);
            var countNewGames = _games.Count();

            Assert.Equal(1, countNewGames);
        }

        [Fact]
        public void UpdateGame_GameDTO_boolUpdateTrue()
        {
            var game = new GameViewModel();

            _gameController.Update(game);

            Assert.True(_isUpdate);
        }

        [Fact]
        public void DeleteGame_Id_boolDeleteTrue()
        {
            _gameController.Remove(Id);

            Assert.True(_isDelete);
        }

        [Fact]
        public void GetGame_Id_boolGetTrue()
        {
            _gameController.GetGameById(Id);

            Assert.True(_isGet);
        }

    }
}
