using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest 
    {
        private readonly Mock<ICommentService> _commentService;
        private readonly CommentController _sut;
        private readonly List<CommentDTO> _fakeListCommentDto;

        private readonly Guid _id;

        public CommentControllerTest()
        {
            Mapper.Reset();

            _id = Guid.NewGuid();
            var mapper = new Mock<IMapper>();
            _commentService = new Mock<ICommentService>();
            _sut = new CommentController(_commentService.Object, mapper.Object);
            _fakeListCommentDto = new List<CommentDTO>{ 
                new CommentDTO{Body = "body1",Game = new GameDTO(),Id=Guid.NewGuid(),Name = "name1",ParentCommentId = null},
                new CommentDTO{Body = "body2",Game = new GameDTO(),Id=Guid.NewGuid(),Name = "name2",ParentCommentId = null}
               };
        }

        [Fact]
        public void AddCommentToGame_ValidComment_CommentAdded()
        {
            _commentService.Setup(service => service.AddComment(It.IsAny<CommentDTO>()));

            _sut.CommentToGame(_id, new CommentViewModel(), null);

            _commentService.Verify(service => service.AddComment(It.IsAny<CommentDTO>()), Times.Once);
        }

        [Fact]
        public void GetAllCommentToGame_ExitingGameId_ReturnsGame()
        {
            _commentService.Setup(service => service.GetCommentsByGameId(_id)).Returns(_fakeListCommentDto);

            var comments = _sut.GetAllCommentToGame(_id);

            Assert.NotNull(comments);
        }
    }
}
