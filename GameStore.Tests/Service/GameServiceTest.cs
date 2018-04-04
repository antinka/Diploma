using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILog> _log;
        private readonly GameService _sut;

        private readonly Guid _id;

        public GameServiceTest()
        {
            _id = Guid.NewGuid();
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _log = new Mock<ILog>();
            _sut = new GameService(_uow.Object, _mapper.Object, _log.Object);
        }

        [Fact]
        public void GetAllGame_GetAllGames_AllGameGeted()
        {
            _uow.Setup(x => x.Games.GetAll());

            _sut.GetAll();

            _uow.VerifyAll();
        }

        [Fact]
        public void GetGame_GameDTO_GameGeted()
        {
            _uow.Setup(x => x.Games.GetById(_id));

            _sut.Get(_id);

            _uow.VerifyAll();
        }
        [Fact]
        public void AddNewGame_Game_NewGameAdded()
        {
            _uow.Setup(x => x.Games.Create(It.IsAny<Game>()));

            _sut.AddNew(new GameDTO());

            _uow.VerifyAll();
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            _uow.Setup(x => x.Games.Update(It.IsAny<Game>()));

            _sut.Update(new GameDTO());

            _uow.VerifyAll();
        }
    }
}
