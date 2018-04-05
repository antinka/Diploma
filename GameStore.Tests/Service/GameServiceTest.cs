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
        private readonly Game _faceGame;
        private readonly Genre _faceGenre;
        private readonly PlatformType _facePlatformType;
        private readonly List<Game> _faceGames;
        private readonly Guid _id;

        public GameServiceTest()
        {
            _id = Guid.NewGuid();
            _uow = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var log = new Mock<ILog>();
            _sut = new GameService(_uow.Object, mapper.Object, log.Object);
            _faceGame = new Game();
            _faceGames = new List<Game>();
            _faceGenre = new Genre();
            _facePlatformType = new PlatformType();
        }

        [Fact]
        public void GetAllGame_GetAllGames_AllGameGeted()
        {
            _uow.Setup(repository => repository.Games.GetAll()).Returns(_faceGames);

            _sut.GetAll();

            Assert.NotNull(_faceGame);
        }

        [Fact]
        public void GetGame_ExistingGameId_GameGeted()
        {
            _uow.Setup(repository => repository.Games.GetById(_id)).Returns(_faceGame);

            _sut.Get(_id);

            Assert.NotNull(_faceGame);
        }

        [Fact]
        public void GetGame_NotExistingGameId_Exeption()
        {
            _uow.Setup(repository => repository.Games.GetById(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.Get(_id));
        }

        [Fact]
        public void AddNewGame_Game_NewGameAdded()
        {
            _uow.Setup(x => x.Games.Create(It.IsAny<Game>()));

            _sut.AddNew(new GameDTO());

            _uow.Verify(repository => repository.Games.Create(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            _uow.Setup(x => x.Games.Update(It.IsAny<Game>()));

            _sut.Update(new GameDTO());

            _uow.Verify(repository => repository.Games.Update(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void DeleteGame_NotExistGameId__ExeptionEntityNotFound()
        {
            _uow.Setup(x => x.Games.Delete(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(_id));
        }

        [Fact]
        public void GetGamesByGenre_NotExistGenreId_ExeptionEntityNotFound()
        {
            _uow.Setup(repository => repository.Genres.GetById(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.GetGamesByGenre(_id));
        }

        [Fact]
        public void GetGamesByPlatformType_NotExistPlatformTypeId_ExeptionEntityNotFound()
        {
            _uow.Setup(repository => repository.PlatformTypes.GetById(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.GetGamesByPlatformType(_id));
        }

        [Fact]
        public void GetGamesByGenre_ExistGenred_GetedGamesByGenre()
        {
            _uow.Setup(repository => repository.Genres.GetById(_id)).Returns(_faceGenre);
            _uow.Setup(repository => repository.Games.GetAll()).Returns(_faceGames);

            var commentsByGameId = _sut.GetGamesByGenre(_id);

            Assert.NotNull(commentsByGameId);
        }

        [Fact]
        public void GetGamesByPlatformType_ExistPlatformTypeId_GetedGamesByPlatformType()
        {
            _uow.Setup(repository => repository.PlatformTypes.GetById(_id)).Returns(_facePlatformType);
            _uow.Setup(repository => repository.Games.GetAll()).Returns(_faceGames);

            var commentsByGameId = _sut.GetGamesByPlatformType(_id);

            Assert.NotNull(commentsByGameId);
        }
    }
}
