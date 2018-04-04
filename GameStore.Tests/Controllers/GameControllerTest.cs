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
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IGameService> _uow;
        private readonly GameController _sut;

        private readonly Guid _id;
        
        public GameControllerTest()
        {
            Mapper.Reset();

            _id = Guid.NewGuid();
            _mapper = new Mock<IMapper>();
            _uow = new Mock<IGameService>();
            _sut = new GameController(_uow.Object, _mapper.Object);

        }

        [Fact]
        public void NewGame_Game_GameAdded()
        {
            var game = new GameViewModel();
            _uow.Setup(x => x.AddNew(It.IsAny<GameDTO>()));

            _sut.New(game);

            _uow.VerifyAll();
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            var game = new GameViewModel();
            _uow.Setup(x => x.Update(It.IsAny<GameDTO>()));

            _sut.Update(game);

            _uow.VerifyAll();
        }

        [Fact]
        public void RemoveGame_IdGame_GameRemoved()
        {
            _uow.Setup(x => x.Delete(_id));

            _sut.Remove(_id);

            _uow.VerifyAll();
        }

        [Fact]
        public void GetGameById_IdGame_GameGetById()
        {
            _uow.Setup(x => x.Get(_id));

            _sut.GetGameById(_id);

            _uow.VerifyAll();
        }

    }
}
