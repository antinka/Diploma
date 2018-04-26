using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GameStore.Infrastructure.Mapper;
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

        private readonly Guid _fakeCommentId, _fakeGameId;
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

            _fakeCommentId = Guid.NewGuid();
            _fakeGameId = Guid.NewGuid(); ;
            _fakeGameKey = _fakeCommentId.ToString();

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
            var fakeGameViewModel = new GameViewModel() { Name = "test", Key = "test" };
            var fakeGameDTO = _mapper.Map<GameDTO>(fakeGameViewModel);

            _gameService.Setup(service => service.AddNew(fakeGameDTO)).Verifiable();

            _sut.New(fakeGameViewModel);

            _gameService.Verify(s => s.AddNew(It.IsAny<GameDTO>()), Times.Once);
        }

        [Fact]
        public void New_InalidGame_ReturnView()
        {
            var fakeGameViewModel = new GameViewModel() { Name = "test", Key = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeGameViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void UpdateGame_Game_HttpStatusCodeOK()
        {
            _gameService.Setup(service => service.Update(It.IsAny<GameDTO>()));

            var httpStatusCodeResult = _sut.Update(It.IsAny<GameViewModel>()) as HttpStatusCodeResult;

            Assert.Equal(200, httpStatusCodeResult.StatusCode);
        }

        [Fact]
        public void GetGame_Gamekey_Verifiable()
        {
            _gameService.Setup(service => service.GetByKey(_fakeGameKey)).Verifiable();

            var res = _sut.GetGame(_fakeGameKey);

            _gameService.Verify(s => s.GetByKey(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RemoveGame_GameKey_HttpStatusCodeOK()
        {
            _gameService.Setup(service => service.Delete(_fakeGameId));

            var httpStatusCodeResult = _sut.Remove(_fakeGameId) as HttpStatusCodeResult;

            Assert.Equal(200, httpStatusCodeResult.StatusCode);
        }

        [Fact]
        public void GetAllGames_ReturnedGames()
        {
            _gameService.Setup(service => service.GetAll()).Returns(_fakeGames);

            var game = _sut.GetAllGames() as ViewResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(game.Model);

            Assert.Equal(_fakeGames.Count, data.Count);
        }

        [Fact]
        public void CountGames_ReturnPartialViewResult()
        {
            _gameService.Setup(service => service.GetAll()).Returns(_fakeGames).Verifiable();

            var game = _sut.CountGames();

            _gameService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
