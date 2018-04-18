using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.Infastracture;
using GameStore.ViewModels;
using Moq;
using System;
using System.Web.Mvc;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest
    {
        private readonly Mock<ICommentService> _commentService;
        private readonly Mock<IGameService> _gameService;
        private readonly Mock<TempDataDictionary> _tempDataMock;
        private readonly IMapper _mapper;
        private readonly CommentController _sut;

        private readonly Guid _fakeCommentId;
        private readonly string _fakeGameKey;

        public CommentControllerTest()
        {
            _commentService = new Mock<ICommentService>();
            _gameService = new Mock<IGameService>();
            _tempDataMock = new Mock<TempDataDictionary>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new CommentController(_commentService.Object, _gameService.Object, _mapper);

            _fakeCommentId = Guid.NewGuid();
            _fakeGameKey = _fakeCommentId.ToString();
        }

        [Fact]
        public void GetAllCommentToGame_GameKey_Verifiable()
        {
            _commentService.Setup(service => service.GetCommentsByGameKey(_fakeGameKey)).Verifiable();
            _sut.TempData = _tempDataMock.Object;

            var res = _sut.GetAllCommentToGame(_fakeGameKey);

            _commentService.Verify(s => s.GetCommentsByGameKey(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteComment_CommentIdAndSure_RedirectToActionResult()
        {
            _commentService.Setup(service => service.Delete(It.IsAny<Guid>()));
            _sut.TempData = _tempDataMock.Object;

            var res = _sut.Delete(Guid.NewGuid(), "Sure") as RedirectToRouteResult;

            Assert.Equal("Comment", res.RouteValues["controller"]);
            Assert.Equal("GetAllCommentToGame", res.RouteValues["action"]);
        }

        [Fact]
        public void BanComment_CommentIdAndPeriod_RedirectToActionResult()
        {
            _commentService.Setup(service => service.Ban(It.IsAny<BanPeriod>(), It.IsAny<Guid>()));

            var res = _sut.Ban(_fakeCommentId, BanPeriod.Day) as RedirectToRouteResult;

            Assert.Equal("Game", res.RouteValues["controller"]);
            Assert.Equal("GetAllGames", res.RouteValues["action"]);
        }

        [Fact]
        public void CommentToGame_ValidComment_RedirectToActionResult()
        {
            var fakeCommentViewModel = new CommentViewModel() { Name = "test", Body = "test", Game = new GameViewModel() };
            var fakeCommentDTO = _mapper.Map<CommentDTO>(fakeCommentViewModel);

            _commentService.Setup(service => service.AddComment(fakeCommentDTO));

            var res = _sut.CommentToGame(fakeCommentViewModel) as RedirectToRouteResult;

            Assert.Equal("Comment", res.RouteValues["controller"]);
            Assert.Equal("GetAllCommentToGame", res.RouteValues["action"]);
        }
    }
}
