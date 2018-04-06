using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.ViewModels;
using Moq;
using System;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class GameControllerTest 
    {
        private readonly Mock<IGameService> _gameService;
        private readonly GameController _sut;
        private readonly GameDTO _fakeGame;
        private readonly Guid _id;
        
        public GameControllerTest()
        {
            Mapper.Reset();

            _id = Guid.NewGuid();
            var mapper = new Mock<IMapper>();
            _gameService = new Mock<IGameService>();
            _sut = new GameController(_gameService.Object, mapper.Object);
            _fakeGame = new GameDTO();
        }

        [Fact]
        public void NewGame_Game_GameAdded()
        {
            _gameService.Setup(service => service.AddNew(It.IsAny<GameDTO>()));

            _sut.New(new GameViewModel());

            _gameService.Verify(service => service.AddNew(It.IsAny<GameDTO>()), Times.Once);
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            _gameService.Setup(service => service.Update(It.IsAny<GameDTO>()));

            _sut.Update(It.IsAny <GameViewModel>());

            _gameService.Verify(service => service.Update(It.IsAny<GameDTO>()), Times.Once);
        }

        [Fact]
        public void RemoveGame_IdGame_GameRemoved()
        {
            _gameService.Setup(service => service.Delete(_id));

            _sut.Remove(_id);

            _gameService.Verify(service => service.Delete(_id), Times.Once);
        }

        [Fact]
        public void GetGameById_ExistingGameId_GameGetById()
        {
            _gameService.Setup(service => service.Get(_id)).Returns(_fakeGame);

            var game =_sut.GetGame(_id);

           Assert.NotNull(game);
        }
    }
}
