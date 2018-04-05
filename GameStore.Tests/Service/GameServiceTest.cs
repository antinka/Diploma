using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using GameStore.BLL.Exeption;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly GameService _sut;
        private readonly Game _game;
        private readonly List<Game> _games;
        private readonly Guid _id;

        public GameServiceTest()
        {
            _id = Guid.NewGuid();
            _uow = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var log = new Mock<ILog>();
            _sut = new GameService(_uow.Object, mapper.Object, log.Object);
            _game = new Game();
            _games = new List<Game>();
        }

        [Fact]
        public void GetAllGame_GetAllGames_AllGameGeted()
        {
            _uow.Setup(x => x.Games.GetAll()).Returns(_games);

            _sut.GetAll();

            Assert.NotNull(_game);
        }

        [Fact]
        public void GetGame_ExistingGameId_GameGeted()
        {
            _uow.Setup(x => x.Games.GetById(_id)).Returns(_game);

            _sut.Get(_id);

           Assert.NotNull(_game);
        }

        [Fact]
        public void GetGame_NotExistingGameId_Exeption()
        {
            _uow.Setup(x => x.Games.GetById(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.Get(_id));
        }

        [Fact]
        public void AddNewGame_Game_NewGameAdded()
        {
            _uow.Setup(x => x.Games.Create(It.IsAny<Game>()));

            _sut.AddNew(new GameDTO());

            _uow.Verify(x => x.Games.Create(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            _uow.Setup(x => x.Games.Update(It.IsAny<Game>()));

            _sut.Update(new GameDTO());

            _uow.Verify(x => x.Games.Update(It.IsAny<Game>()), Times.Once);
        }
    }
}
