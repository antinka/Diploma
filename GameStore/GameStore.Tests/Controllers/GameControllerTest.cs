using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Web.Builder.Implementation;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Games;
using Moq;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class GameControllerTest
    {
        private readonly Mock<IGameService> _gameService;
        private readonly Mock<IGenreService> _genreService;
        private readonly Mock<IPlatformTypeService> _platformTypeService;
        private readonly Mock<IPublisherService> _publisherService;
        private readonly IMapper _mapper;
        private readonly GameController _sut;
        private readonly FilterViewModelBuilder _filterViewModelBuilder;

        private readonly Guid _fakeGameId;
        private readonly string _fakeGameKey;
        private readonly List<GameDTO> _fakeGames;

        public GameControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _gameService = new Mock<IGameService>();
            _genreService = new Mock<IGenreService>();
            _platformTypeService = new Mock<IPlatformTypeService>();
            _publisherService = new Mock<IPublisherService>();
            _filterViewModelBuilder = new FilterViewModelBuilder(
                _genreService.Object,
                _platformTypeService.Object,
                _mapper,
                _publisherService.Object);
            _sut = new GameController(
                _gameService.Object,
                _genreService.Object,
                _platformTypeService.Object,
                _mapper, 
                _publisherService.Object,
                _filterViewModelBuilder,
                null);

            var fakeCommentId = Guid.NewGuid();
            _fakeGameId = Guid.NewGuid();
            _fakeGameKey = fakeCommentId.ToString();

            _fakeGames = new List<GameDTO>
            {
                new GameDTO()
                {
                    Id = Guid.NewGuid(),
                    Key = "1"
                },
                new GameDTO()
                {
                    Id = Guid.NewGuid(),
                    Key = "2"
                }
            };
        }

        [Fact]
        public void New_ValidGame_AddNewCalled()
        {
            var fakeGameViewModel = new GameViewModel()
            {
                NameEn = "test",
                Key = "test",
                SelectedGenresName = new List<string>(),
                SelectedPlatformTypesName = new List<string>()
            };
            var fakeGameDTO = _mapper.Map<ExtendGameDTO>(fakeGameViewModel);

            _gameService.Setup(service => service.IsUniqueKey(It.IsAny<ExtendGameDTO>())).Returns(true);
            _gameService.Setup(service => service.AddNew(fakeGameDTO));

            _sut.New(fakeGameViewModel);

            _gameService.Verify(s => s.AddNew(It.IsAny<ExtendGameDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidGame_ReturnViewResult()
        {
            var fakeGameViewModel = new GameViewModel() { NameEn = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeGameViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Update_ValidGame_UpdateCalled()
        {
            var fakeGameViewModel = new GameViewModel()
            {
                NameEn = "test",
                Key = "test",
                SelectedGenresName = new List<string>(),
                SelectedPlatformTypesName = new List<string>()
            };
            var fakeGameDTO = _mapper.Map<ExtendGameDTO>(fakeGameViewModel);

            _gameService.Setup(service => service.IsUniqueKey(It.IsAny<ExtendGameDTO>())).Returns(true);
            _gameService.Setup(service => service.Update(fakeGameDTO));

            _sut.Update(fakeGameViewModel);

            _gameService.Verify(s => s.Update(It.IsAny<ExtendGameDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidGame_ReturnViewResult()
        {
            var fakeGameViewModel = new GameViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakeGameViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Update_Gamekey_ReturnView()
        {
            var fakeGame = new ExtendGameDTO() { Id = Guid.NewGuid(), Key = _fakeGameKey, NameEn = "test" };

            _gameService.Setup(service => service.GetByKey(_fakeGameKey)).Returns(fakeGame);
            _publisherService.Setup(service => service.GetAll()).Returns(new List<PublisherDTO>());

            var res = _sut.Update(_fakeGameKey);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Get_Gamekey_GetByKeyCalled()
        {
            var fakeGame = new ExtendGameDTO() { Id = Guid.NewGuid(), Key = _fakeGameKey, NameEn = "test" };
            _gameService.Setup(service => service.GetByKey(_fakeGameKey)).Returns(fakeGame); 
            
            _sut.GetGame(_fakeGameKey);

            _gameService.Verify(s => s.GetByKey(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Remove_GameId_DeleteCalled()
        {
            _gameService.Setup(service => service.Delete(_fakeGameId));

            _sut.Remove(_fakeGameId);

            _gameService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnViewResult()
        {
            _gameService.Setup(service => service.GetAll()).Returns(_fakeGames);

            var res = _sut.GetAllDeleteGames();

           Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void CountGames_GetCountGameCalled()
        {
            _gameService.Setup(service => service.GetCountGame()).Returns(5);

            _sut.CountGames();

            _gameService.Verify(s => s.GetCountGame(), Times.Once);
        }

        [Fact]
        public void New_ReturnViewResult()
        {
            var res = _sut.New();

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void New_GameWithoutUnickKey_IsUniqueKeyCalled()
        {
            var fakeGameViewModel = new GameViewModel() { NameEn = "test", Key = "1" };
            _gameService.Setup(service => service.IsUniqueKey(It.IsAny<ExtendGameDTO>())).Returns(false);

            _sut.New(fakeGameViewModel);

            _gameService.Verify(s => s.IsUniqueKey(It.IsAny<ExtendGameDTO>()), Times.Once);
        }

        [Fact]
        public void Update_GameWithoutUnickKey_IsUniqueKeyCalled()
        {
            var fakeGameViewModel = new GameViewModel() { NameEn = "test", Key = "1" };
            _gameService.Setup(service => service.IsUniqueKey(It.IsAny<ExtendGameDTO>())).Returns(false);

            _sut.Update(fakeGameViewModel);

            _gameService.Verify(s => s.IsUniqueKey(It.IsAny<ExtendGameDTO>()), Times.Once);
        }

        [Fact]
        public void GamesFilters_EmptyFilterViewModel_ReturnedlViewResult()
        {
            var filterViewModel = new FilterViewModel()
            {
                SearchGameName = "name",
                MaxPrice = 20,
                MinPrice = 10,
                PageSize = PageSize.All
            };

            var res = _sut.FilteredGames(filterViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void FilteredGames_EmptyFilterViewModel_ReturnedlViewResult()
        {
            var filterViewModel = new FilterViewModel()
            {
                SearchGameName = "name",
                MaxPrice = 20,
                MinPrice = 10,
            };

            var res = _sut.FilteredGames(filterViewModel);

            Assert.IsType<ViewResult>(res);
        }
    }
}
