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
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IGameService> _uow;
        private readonly GameController _sut;

        public Guid Id = Guid.NewGuid();
        
        public GameControllerTest()
        {
            Mapper.Reset();
            mapper = new Mock<IMapper>();
            _uow = new Mock<IGameService>();
            _sut = new GameController(_uow.Object, mapper.Object);

        }

        [Fact]
        public void NewGame_Game_VerifyAll()
        {
            var game = new GameViewModel();
            _uow.Setup(x => x.AddNewGame(It.IsAny<GameDTO>()));

            _sut.New(game);

            _uow.VerifyAll();
        }

        [Fact]
        public void UpdateGame_Game_VerifyAll()
        {
            var game = new GameViewModel();
            _uow.Setup(x => x.UpdateGame(It.IsAny<GameDTO>()));

            _sut.Update(game);

            _uow.VerifyAll();
        }

        [Fact]
        public void RemoveGame_IdGame_VerifyAll()
        {
            _uow.Setup(x => x.DeleteGame(Id));

            _sut.Remove(Id);

            _uow.VerifyAll();
        }

        [Fact]
        public void GetGameById_IdGame_VerifyAll()
        {
            _uow.Setup(x => x.GetGame(Id));

            _sut.GetGameById(Id);

            _uow.VerifyAll();
        }

    }
}
