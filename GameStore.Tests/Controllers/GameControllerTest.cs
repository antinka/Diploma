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
        private static readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private static readonly Mock<IGameService> gameRepo = new Mock<IGameService>();
        private readonly GameController _sut = new GameController(gameRepo.Object, mapper.Object);

        private readonly List<GameDTO> _games = new List<GameDTO>();
        public Guid Id = Guid.NewGuid();
        private bool _isDelete, _isUpdate, _isGet;
        

        public GameControllerTest()
        {
            Mapper.Reset();
            gameRepo.Setup(x => x.AddNewGame(It.IsAny<GameDTO>())).Callback(() => _games.Add(It.IsAny<GameDTO>()));
            gameRepo.Setup(x => x.UpdateGame(It.IsAny<GameDTO>())).Callback(() => _isUpdate = true);
            gameRepo.Setup(x => x.DeleteGame(It.IsAny<Guid>())).Callback(() => _isDelete = true);
            gameRepo.Setup(x => x.GetGame(It.IsAny<Guid>())).Callback(() => _isGet = true);
        }

        [Fact]
        public void NewGame_Game_NewGameInList()
        {
            var game = new GameViewModel();

            _sut.New(game);
            var countNewGames = _games.Count();

            Assert.Equal(1, countNewGames);
        }

        [Fact]
        public void UpdateGame_Game_TrueIsUpdate()
        {
            var game = new GameViewModel();

            _sut.Update(game);

            Assert.True(_isUpdate);
        }

        [Fact]
        public void RemoveGame_IdGame_TrueIsDelete()
        {
            _sut.Remove(Id);

            Assert.True(_isDelete);
        }

        [Fact]
        public void GetGameById_IdGame_TrueIsGetGameById()
        {
            _sut.GetGameById(Id);

            Assert.True(_isGet);
        }

    }
}
