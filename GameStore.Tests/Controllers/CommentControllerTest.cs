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
        private readonly Mock<TempDataDictionary> _tempDataMock;
        private readonly IMapper _mapper;
        private readonly CommentController _sut;

        private readonly List<CommentDTO> _fakeComments;
        private readonly Guid _gamekey;

        public CommentControllerTest()
        {
            _commentService = new Mock<ICommentService>();
            _tempDataMock = new Mock<TempDataDictionary>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new CommentController(_commentService.Object, _mapper);

            _gamekey = Guid.NewGuid();

            _fakeComments = new List<CommentDTO>{
                new CommentDTO
                {
                    Body = "body1",
                    Game = new GameDTO(),
                    Id = Guid.NewGuid(),
                    Name = "name1",
                    ParentCommentId = null
                },
                new CommentDTO
                {
                    Body = "body2",
                    Game = new GameDTO(),
                    Id = Guid.NewGuid(),
                    Name = "name2",
                    ParentCommentId = null
                }
               };
        }

        [Fact]
        public void BanComment_CommentIdAndPeriod_RedirectToActionResult()
        {
            _commentService.Setup(service => service.Ban(It.IsAny<BanPeriod>(), It.IsAny<Guid>()));

            var httpStatusCodeResult = _sut.AddCommentToGame(_gamekey, new CommentViewModel()) as HttpStatusCodeResult;

            Assert.Equal("Game", res.RouteValues["controller"]);
            Assert.Equal("GetAllGames", res.RouteValues["action"]);
        }

        [Fact]
        public void CommentToGame_ValidComment_RedirectToActionResult()
        {
            _commentService.Setup(service => service.GetCommentsByGameId(_gamekey)).Returns(_fakeComments);

            var commentResult = _sut.GetAllCommentToGame(_gamekey) as JsonResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(commentResult.Data);

            Assert.Equal("Comment", res.RouteValues["controller"]);
            Assert.Equal("GetAllCommentToGame", res.RouteValues["action"]);
        }
    }
}
