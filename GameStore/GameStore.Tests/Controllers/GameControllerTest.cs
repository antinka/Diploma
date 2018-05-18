using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GameStore.Web.ViewModels.Games;
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
            _sut = new GameController(_gameService.Object, _genreService.Object,
                _platformTypeService.Object, _mapper, _publisherService.Object);

            var fakeCommentId = Guid.NewGuid();
            _fakeGameId = Guid.NewGuid(); ;
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
        public void New_ValidGame_Verifiable()
        {
            var fakeGameViewModel = new GameViewModel() { Name = "test", Key = "test" , SelectedGenresName = new List<string>(), SelectedPlatformTypesName  = new List<string>()};
            var fakeGameDTO = _mapper.Map<GameDTO>(fakeGameViewModel);

            _gameService.Setup(service => service.IsUniqueKey(It.IsAny<GameDTO>())).Returns(true);
            _gameService.Setup(service => service.AddNew(fakeGameDTO)).Verifiable();

            _sut.New(fakeGameViewModel);

            _gameService.Verify(s => s.AddNew(It.IsAny<GameDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidGame_ReturnViewResult()
        {
            var fakeGameViewModel = new GameViewModel() { Name = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeGameViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void UpdateGame_ValidGame_Verifiable()
        {
            var fakeGameViewModel = new GameViewModel() { Name = "test", Key = "test", SelectedGenresName = new List<string>(), SelectedPlatformTypesName = new List<string>() };
            var fakeGameDTO = _mapper.Map<GameDTO>(fakeGameViewModel);

            _gameService.Setup(service => service.IsUniqueKey(It.IsAny<GameDTO>())).Returns(true);
            _gameService.Setup(service => service.Update(fakeGameDTO)).Verifiable();

            _sut.Update(fakeGameViewModel);

            _gameService.Verify(s => s.Update(It.IsAny<GameDTO>()), Times.Once);
        }

        [Fact]
        public void UpdateGame_InvalidGame_ReturnViewResult()
        {
            var fakeGameViewModel = new GameViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakeGameViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void UpdateGame_Gamekey_ReturnView()
        {
            var fakeGame = new GameDTO() { Id = Guid.NewGuid(), Key = _fakeGameKey, Name = "test" };

            _gameService.Setup(service => service.GetByKey(_fakeGameKey)).Returns(fakeGame);
            _publisherService.Setup(service => service.GetAll()).Returns(new List<PublisherDTO>());

            var res = _sut.Update(_fakeGameKey);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void GetGame_Gamekey_Verifiable()
        {
            _gameService.Setup(service => service.GetByKey(_fakeGameKey)).Verifiable();

            _sut.GetGame(_fakeGameKey);

            _gameService.Verify(s => s.GetByKey(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RemoveGame_GameId_Verifiable()
        {
            _gameService.Setup(service => service.Delete(_fakeGameId)).Verifiable();

            _sut.Remove(_fakeGameId);

            _gameService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAllGames_ReturnViewResult()
        {
            _gameService.Setup(service => service.GetAll()).Returns(_fakeGames);

            var res = _sut.GetAllGames();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void CountGames_ReturnPartialViewResult()
        {
            _gameService.Setup(service => service.GetCountGame()).Returns(5).Verifiable();

            _sut.CountGames();

            _gameService.Verify(s => s.GetCountGame(), Times.Once);
        }

        [Fact]
        public void New_ReturnViewResult()
        {
            var res = _sut.New();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
