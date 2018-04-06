using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Exeption;
using GameStore.Infastracture;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;

        private readonly GameService _sut;
        private readonly IMapper _mapper;

        private readonly Game _fakeGame;
        private readonly Genre _fakeGenre;
        private readonly PlatformType _facePlatformType;
        private readonly List<Game> _fakeGames;
        private readonly Guid _id;
        

        public GameServiceTest()
        {
            _id = Guid.NewGuid();
            _uow = new Mock<IUnitOfWork>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            var log = new Mock<ILog>();
            _sut = new GameService(_uow.Object, _mapper, log.Object);
            _fakeGame = new Game(){ Id = _id };
            _fakeGames = new List<Game>();
            _fakeGenre = new Genre();
            _facePlatformType = new PlatformType();
        }

        [Fact]
        public void GetAllGame_GetAllGames_AllGameGeted()
        {
            _uow.Setup(repository => repository.Games.GetAll()).Returns(_fakeGames);

            var resultGames = _sut.GetAll();

            Assert.Equal(resultGames.Count(), _fakeGames.Count);
        }

        [Fact]
        public void GetGame_ExistingGameId_GameGeted()
        {
            _uow.Setup(repository => repository.Games.GetById(_id)).Returns(_fakeGame);

            var resultGame = _sut.Get(_id);

            Assert.NotNull(resultGame);
        }

        //todo fix
        [Fact]
        public void GetGame_NotExistingGameId_Exeption()
        {
            _uow.Setup(repository => repository.Games.GetById(_id)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.Get(_id));
        }

        [Fact]
        public void AddNewGame_Game_NewGameAdded()
        {
            var fakeGameDTO = new GameDTO(){ Key = "qq"};
            var fakeGame = _mapper.Map<Game>(fakeGameDTO);

            _uow.Setup(uof => uof.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>() { new Game() { Key = "qq" } });
            _uow.Setup(x => x.Games.Create(fakeGame)).Verifiable();
            

            _sut.AddNew(fakeGameDTO);

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
            _uow.Setup(repository => repository.Genres.GetById(_id)).Returns(null as Genre);

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
            _uow.Setup(repository => repository.Genres.GetById(_id)).Returns(_fakeGenre);
            _uow.Setup(repository => repository.Games.GetAll()).Returns(_fakeGames);

            var commentsByGameId = _sut.GetGamesByGenre(_id);

            //Assert.True(commentsByGameId.All(x=>x.Genres.any(x)));
        }

        [Fact]
        public void GetGamesByPlatformType_ExistPlatformTypeId_GetedGamesByPlatformType()
        {
            _uow.Setup(repository => repository.PlatformTypes.GetById(_id)).Returns(_facePlatformType);
            _uow.Setup(repository => repository.Games.GetAll()).Returns(_fakeGames);

            var commentsByGameId = _sut.GetGamesByPlatformType(_id);

            Assert.NotNull(commentsByGameId);
        }
    }
}
