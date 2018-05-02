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
using GameStore.Infrastructure.Mapper;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly GameService _sut;
        private readonly IMapper _mapper;

        private readonly string _fakeGameKey;
        private readonly Game _fakeGame;
        private readonly Genre _fakeGenre;
        private readonly PlatformType _fakePlatformType;
        private readonly List<Game> _fakeGames;
        private readonly Guid _fakeGameId, _fakeGenreId, _fakePlatformTypeId;

        public GameServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new GameService(_uow.Object, _mapper, log.Object);

            _fakeGameKey = Guid.NewGuid().ToString();
            _fakeGameId = Guid.NewGuid();
            _fakeGenreId = Guid.NewGuid();
            _fakePlatformTypeId = Guid.NewGuid();

            _fakeGenre = new Genre
            {
                Id = _fakeGenreId,
                Name = "genre1"
            };

            var fakeGenres = new List<Genre>()
            {
                _fakeGenre,

                new Genre() { Id = new Guid() }
            };

            _fakePlatformType = new PlatformType()
            {
                Id = _fakePlatformTypeId,
                Name = "platformType1"
            };

            var fakePlatformTypes = new List<PlatformType>()
            {
                _fakePlatformType
            };

            _fakeGame = new Game()
            {
                Id = _fakeGameId,
                Key = _fakeGameKey,
                Genres = fakeGenres,
                PlatformTypes = fakePlatformTypes
            };

            _fakeGames = new List<Game>
            {
                _fakeGame
            };
        }

        [Fact]
        public void AddNewGame_GameWithUniqueKey_NewGameAdded()
        {
            var fakeGameDTO = new GameDTO() { Id = Guid.NewGuid(), Key = "qweqwe", Name = "1", Description = "2" };
            var fakeGame = _mapper.Map<Game>(fakeGameDTO);

            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>());
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());
            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(new List<PlatformType>());

            _uow.Setup(uow => uow.Games.Create(fakeGame)).Verifiable();

            _sut.AddNew(fakeGameDTO);

            _uow.Verify(uow => uow.Games.Create(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void AddNewGame_GameWithoutUniqueKey_ExeptionEntityNotFound()
        {
            var fakeGameDTO = new GameDTO() { Id = Guid.NewGuid(), Key = _fakeGameKey };

            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            Assert.Throws<NotUniqueParameter>(() => _sut.AddNew(fakeGameDTO));
        }

        [Fact]
        public void GetAllGame_AllGamesReturned()
        {
            _uow.Setup(uow => uow.Games.GetAll()).Returns(_fakeGames);

            var resultGames = _sut.GetAll();

            Assert.Equal(resultGames.Count(), _fakeGames.Count);
        }

        [Fact]
        public void GetGameById_ExistedGameId_GameReturned()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);

            var resultGame = _sut.GetById(_fakeGameId);

            Assert.True(resultGame.Id == _fakeGameId);
        }

        [Fact]
        public void GetGameById_NotExistedGameKey_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.GetById(_fakeGameId));
        }

        [Fact]
        public void GetGameByKey_ExistedGameKey_GameReturned()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            var resultGame = _sut.GetByKey(_fakeGameKey);

            Assert.True(resultGame.Key == _fakeGameKey);
        }

        [Fact]
        public void GetGameByKey_NotExistedGameKey_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>());

            Assert.Throws<EntityNotFound>(() => _sut.GetByKey(_fakeGameKey));
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            var fakeGameDTO = new GameDTO() { Id = _fakeGameId, Key = "123" };
            var fakeGame = _mapper.Map<Game>(fakeGameDTO);

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(fakeGame);
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());
            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(new List<PlatformType>());
            _uow.Setup(uow => uow.Games.Update(fakeGame)).Verifiable();

            _sut.Update(fakeGameDTO);

            _uow.Verify(uow => uow.Games.Update(It.IsAny<Game>()), Times.Exactly(2));
        }

        [Fact]
        public void UpdateGame_NotExistGame_EntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.Update(new GameDTO()));
        }

        [Fact]
        public void DeleteGame_NotExistedGameId__ExeptionEntityNotFound()
        {
            var notExistGameId = Guid.NewGuid();

            _uow.Setup(uow => uow.Games.GetById(notExistGameId)).Returns(null as Game);
            _uow.Setup(uow => uow.Games.Delete(notExistGameId));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(_fakeGameId));
        }

        [Fact]
        public void DeleteGame_ExistedGameId__Verifiable()
        {
            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Games.Delete(_fakeGameId)).Verifiable();

            _sut.Delete(_fakeGameId);

            _uow.Verify(uow => uow.Games.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetGamesByGenre_NotExistedGenreId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Genres.GetById(_fakeGameId)).Returns(null as Genre);

            Assert.Throws<EntityNotFound>(() => _sut.GetGamesByGenre(_fakeGameId));
        }

        [Fact]
        public void GetGamesByPlatformType_NotExistedPlatformTypeId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.PlatformTypes.GetById(_fakeGameId)).Returns(null as PlatformType);

            Assert.Throws<EntityNotFound>(() => _sut.GetGamesByPlatformType(_fakeGameId));
        }

        [Fact]
        public void GetGamesByGenre_ExistedGenred_ReturnedGamesByGenre()
        {
            _uow.Setup(uow => uow.Genres.GetById(_fakeGenreId)).Returns(_fakeGenre);
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            var getGamesByGenreId = _sut.GetGamesByGenre(_fakeGenreId);

            Assert.Equal(_fakeGames.Count, getGamesByGenreId.Count(g => g.Genres.Any(x => x.Id == _fakeGenreId)));
        }

        [Fact]
        public void GetGamesByPlatformType_ExistedPlatformTypeId_ReturnedGamesByPlatformType()
        {
            _uow.Setup(uow => uow.PlatformTypes.GetById(_fakePlatformTypeId)).Returns(_fakePlatformType);
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            var getGamesByPlatformTypeId = _sut.GetGamesByPlatformType(_fakePlatformTypeId);

            Assert.Equal(_fakeGames.Count, getGamesByPlatformTypeId.Count(g => g.PlatformTypes.Any(x => x.Id == _fakePlatformTypeId)));
        }

        [Fact]
        public void GetCountGame()
        {
            var countGames = 5;
            _uow.Setup(uow => uow.Games.Count()).Returns(countGames);

            var res = _sut.GetCountGame();

            Assert.Equal(countGames, res);
        }
    }
}
