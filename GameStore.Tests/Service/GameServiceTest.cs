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

        private readonly string _fakeGameKey;
        private readonly Game _fakeGame;
        private readonly Genre _fakeGenre;
        private readonly PlatformType _fakePlatformType;
        private readonly List<Game> _fakeGames;
        private readonly Guid _fakeGameId, _fakeGenreId, _fakePlatformTypeId;
        private readonly string _gameKey;

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
            _gameKey = Guid.NewGuid().ToString();

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
                Key = "123",
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

            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(null as IEnumerable<Game>);
            _uow.Setup(uow => uow.Genres.Find(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());
            _uow.Setup(uow => uow.PlatformTypes.Find(It.IsAny<Func<PlatformType, bool>>())).Returns(new List<PlatformType>());

            _uow.Setup(uow => uow.Games.Create(fakeGame)).Verifiable();

            _sut.AddNew(fakeGameDTO);

            _uow.Verify(uow => uow.Games.Create(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void AddNewGame_GameWithoutUniqueKey_ExeptionEntityNotFound()
        {
            var fakeGameDTO = new GameDTO() { Id = Guid.NewGuid(), Key = "123" };
            var fakeGame = _mapper.Map<Game>(fakeGameDTO);

            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            Assert.Throws<EntityNotFound>(() => _sut.AddNew(fakeGameDTO));
        }

        [Fact]
        public void GetAllGame_AllGamesReturned()
        {
            _uow.Setup(uow => uow.Games.GetAll()).Returns(_fakeGames);

            var resultGames = _mapper.Map<IList<GameDTO>>(_sut.GetAll());

            Assert.Equal(resultGames.Count(), _fakeGames.Count);
        }

        [Fact]
        public void GetGame_ExistedGameId_GameReturned()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            var resultGame = _sut.Get(_fakeGameId);

            Assert.True(resultGame.Key == _gameKey);
        }

        [Fact]
        public void GetGame_NotExistedGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(null as List<Game>);

            Assert.Throws<EntityNotFound>(() => _sut.Get(_fakeGameId));
        }

        [Fact]
        public void UpdateGame_Game_GameUpdated()
        {
            var fakeGameDTO = new GameDTO() { Id = Guid.NewGuid(), Key = "123" };
            var fakeGame = _mapper.Map<Game>(fakeGameDTO);

            _uow.Setup(uow => uow.Games.Update(fakeGame)).Verifiable();

            _sut.Update(fakeGameDTO);

            _uow.Verify(uow => uow.Games.Update(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void DeleteGame_NotExistedGameId__ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(null as List<Game>);

            Assert.Throws<EntityNotFound>(() => _sut.Delete(_gameKey));
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
    }
}
