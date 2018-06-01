using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.Web.Infrastructure.Mapper;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Implementation;
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
        private readonly Publisher _fakePublisher;
        private readonly PlatformType _fakePlatformType;
        private readonly List<Game> _fakeGames, _fakeGamesForFilter;
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

            _fakePublisher = new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Publisher"
            };

            var fakeGenres = new List<Genre>()
            {
                _fakeGenre,

                new Genre()
                {
                    Id = new Guid(),
                    Name = "genre1"
                }
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
                PlatformTypes = fakePlatformTypes,
                Name = "game",
                Price = 10,
                Publisher = _fakePublisher,
                PublishDate = DateTime.Today,
                Views = 200
            };

            _fakeGames = new List<Game>
            {
                _fakeGame
            };

            _fakeGamesForFilter = new List<Game>
            {
                _fakeGame,

                new Game()
                {
                    Id = Guid.NewGuid(),
                    Key = Guid.NewGuid().ToString(),
                    Name = "gamegame",
                    Genres = fakeGenres,
                    PlatformTypes = fakePlatformTypes,
                    Price = 15,
                    Publisher = _fakePublisher,
                    PublishDate = DateTime.Today.AddMonths(-1),
                    Views = 10
                }
            };
        }

        [Fact]
        public void AddNewGame_GameWithUniqueKey_Verifiable()
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
            _uow.Setup(uow => uow.Games.Update(It.IsAny<Game>()));

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
            _uow.Setup(uow => uow.Games.Update(It.IsAny<Game>()));

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
        public void UpdateGame_Game_Verifiable()
        {
            var oldFakeGameDTO = new GameDTO() { Id = _fakeGameId, Key = "123", Price = 55 };
            var oldFakeGame = _mapper.Map<Game>(oldFakeGameDTO);
            var newFakeGameDTO = new GameDTO() { Id = _fakeGameId, Key = "12", Price = 5 };
            var newFakeGame = _mapper.Map<Game>(newFakeGameDTO);
            var fakeOrderDetail = new OrderDetail();

            _uow.Setup(uow => uow.Games.GetById(_fakeGameId)).Returns(oldFakeGame);
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());
            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(new List<PlatformType>());
            _uow.Setup(uow => uow.OrderDetails.Get(It.IsAny<Func<OrderDetail, bool>>())).Returns(new List<OrderDetail>() { fakeOrderDetail });
            _uow.Setup(uow => uow.OrderDetails.Update(fakeOrderDetail));
            _uow.Setup(uow => uow.Games.Update(newFakeGame)).Verifiable();

            _sut.Update(newFakeGameDTO);

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

        [Fact]
        public void GetGamesByFilter_FilterByGenre_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SelectedGenresName = new List<string>()
                {
                    "genre1"
                }
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByGenre(filterDto.SelectedGenresName));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.Equal(_fakeGamesForFilter.Count, gamesAfteFilter.Count());
        }

        [Fact]
        public void GetGamesByFilter_FilterByName_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SearchGameName = "game"
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByName(filterDto.SearchGameName));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.Equal(_fakeGamesForFilter.Count, gamesAfteFilter.Count());
        }

        [Fact]
        public void GetGamesByFilter_FilterByPlatform_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SelectedPlatformTypesName = new List<string>()
                {
                    "platformType1"
                }
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByPlatform(filterDto.SelectedPlatformTypesName));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.Equal(_fakeGamesForFilter.Count, gamesAfteFilter.Count());
        }

        [Fact]
        public void GetGamesByFilter_FilterByFilterByMaxPrice_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                MinPrice = 5
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByPrice(null, filterDto.MinPrice.Value));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.Equal(_fakeGamesForFilter.Count, gamesAfteFilter.Count());
        }

        [Fact]
        public void GetGamesByFilter_FilterByFilterByMinPrice_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                MaxPrice = 30
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByPrice(filterDto.MaxPrice.Value, null));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.Equal(_fakeGamesForFilter.Count, gamesAfteFilter.Count());
        }

        [Fact]
        public void GetGamesByFilter_FilterByPublisher_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SelectedPublishersName = new List<string>()
               {
                   "Publisher"
               }
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByPublisher(filterDto.SelectedPublishersName));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.Equal(_fakeGamesForFilter.Count, gamesAfteFilter.Count());
        }

        [Fact]
        public void GetGamesByFilter_SortFilterPriceDesc_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SortType = SortType.PriceDesc
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameSortFilter(filterDto.SortType));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).Price > gamesAfteFilter.ElementAt(1).Price);
        }

        [Fact]
        public void GetGamesByFilter_SortFilterPriceAsc_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SortType = SortType.PriceAsc
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameSortFilter(filterDto.SortType));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).Price < gamesAfteFilter.ElementAt(1).Price);
        }

        [Fact]
        public void GetGamesByFilter_SortFilterMostPopular_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SortType = SortType.MostPopular
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameSortFilter(filterDto.SortType));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).Views > gamesAfteFilter.ElementAt(1).Views);
        }

        [Fact]
        public void GetGamesByFilter_FilterByPageSizeTen_GetedGames()
        {
            var fakeGamesForFilter = new List<Game>()
            {
                new Game { Id = Guid.NewGuid(), Name = "Игра1", Key = "Игра1"},
                new Game { Id = Guid.NewGuid(), Name = "Игра2", Key = "Игра2"},
                new Game { Id = Guid.NewGuid(), Name = "Игра3", Key = "Игра3"},
                new Game { Id = Guid.NewGuid(), Name = "Игра4", Key = "Игра4"},
                new Game { Id = Guid.NewGuid(), Name = "Игра5", Key = "Игра5"},
                new Game { Id = Guid.NewGuid(), Name = "Игра6", Key = "Игра6"},
                new Game { Id = Guid.NewGuid(), Name = "Игра7", Key = "Игра7"},
                new Game { Id = Guid.NewGuid(), Name = "Игра8", Key = "Игра8"},
                new Game { Id = Guid.NewGuid(), Name = "Игра9", Key = "Игра9"},
                new Game { Id = Guid.NewGuid(), Name = "Игра10", Key = "Игра10"},
                new Game { Id = Guid.NewGuid(), Name = "Игра11", Key = "Игра11"},
            };
            GamePipeline gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByPage(2, PageSize.Ten));
            var gamesAfteFilter = gamePipeline.Process(fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).Name == "Игра11");
        }

        [Fact]
        public void GetGamesByFilter_FilterByPageSizeAll_GetedGames()
        {
            var fakeGamesForFilter = new List<Game>()
            {
                new Game { Id = Guid.NewGuid(), Name = "Игра1", Key = "Игра1"},
                new Game { Id = Guid.NewGuid(), Name = "Игра2", Key = "Игра2"},
                new Game { Id = Guid.NewGuid(), Name = "Игра3", Key = "Игра3"},
                new Game { Id = Guid.NewGuid(), Name = "Игра4", Key = "Игра4"},
                new Game { Id = Guid.NewGuid(), Name = "Игра5", Key = "Игра5"},
                new Game { Id = Guid.NewGuid(), Name = "Игра6", Key = "Игра6"},
                new Game { Id = Guid.NewGuid(), Name = "Игра7", Key = "Игра7"},
                new Game { Id = Guid.NewGuid(), Name = "Игра8", Key = "Игра8"},
                new Game { Id = Guid.NewGuid(), Name = "Игра9", Key = "Игра9"},
                new Game { Id = Guid.NewGuid(), Name = "Игра10", Key = "Игра10"},
                new Game { Id = Guid.NewGuid(), Name = "Игра11", Key = "Игра11"},
            };
            GamePipeline gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByPage(1, PageSize.All));
            var gamesAfteFilter = gamePipeline.Process(fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(10).Name == "Игра11");
        }

        [Fact]
        public void IsUniqueKey_UniqueKey_True()
        {
            var game = new GameDTO() { Id = Guid.NewGuid(), Key = _fakeGameKey };
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>());

            var res = _sut.IsUniqueKey(game);

            Assert.True(res);
        }

        [Fact]
        public void IsUniqueKey_NotUniqueKey_False()
        {
            var game = new GameDTO() { Id = Guid.NewGuid(), Key = _fakeGameKey };
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(_fakeGames);

            var res = _sut.IsUniqueKey(game);

            Assert.False(res);
        }

        [Fact]
        public void GetGamesByFilter_FilterDTO_returnedGamesDTO()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                SortType = SortType.MostPopular
            };

            _uow.Setup(uow => uow.Games.GetAll()).Returns(_fakeGames);

            var res = _sut.GetGamesByFilter(filterDto);

            Assert.True(res.Any());
        }

        [Fact]
        public void GetGamesByFilter_FilterByDateThreeYear_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                FilterDate = FilterDate.threeYear
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByDate(filterDto.FilterDate));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).PublishDate > gamesAfteFilter.ElementAt(1).PublishDate);
        }
        [Fact]
        public void GetGamesByFilter_FilterByDateMonth_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                FilterDate = FilterDate.month
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByDate(filterDto.FilterDate));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).PublishDate >= DateTime.Today.AddMonths(-1));
        }

        [Fact]
        public void GetGamesByFilter_FilterByDateWeek_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                FilterDate = FilterDate.week
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByDate(filterDto.FilterDate));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.First().PublishDate >= DateTime.Today.AddDays(-7));
        }

        [Fact]
        public void GetGamesByFilter_FilterByDateOneYear_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                FilterDate = FilterDate.oneYear
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByDate(filterDto.FilterDate));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).PublishDate > gamesAfteFilter.ElementAt(1).PublishDate);
        }

        [Fact]
        public void GetGamesByFilter_FilterByDateTwoYear_GetedGames()
        {
            FilterDTO filterDto = new FilterDTO()
            {
                FilterDate = FilterDate.twoYear
            };
            var gamePipeline = new GamePipeline();

            gamePipeline.Register(new GameFilterByDate(filterDto.FilterDate));
            var gamesAfteFilter = gamePipeline.Process(_fakeGamesForFilter);

            Assert.True(gamesAfteFilter.ElementAt(0).PublishDate > gamesAfteFilter.ElementAt(1).PublishDate);
        }
    }
}
