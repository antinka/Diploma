using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Controllers;
using GameStore.Models;
using Moq;
using System;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest 
    {
        private static readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private static readonly Mock<ICommentService> uow = new Mock<ICommentService>();
        private readonly Guid _id = Guid.NewGuid();
        private bool _isAddCommentToGame = false;
        private bool _isGetAllCommentToGameId = false;
        private readonly CommentController _commentController = new CommentController(uow.Object, mapper.Object);

        public CommentControllerTest()
        {
            Mapper.Reset();
            uow.Setup(x => x.AddComment(It.IsAny<CommentDTO>(),null)).Callback(() => _isAddCommentToGame=true);
            uow.Setup(x => x.GetAllComments(It.IsAny<Guid>())).Callback(() => _isGetAllCommentToGameId = true);
        }

        [Fact]
        public void AddCommentToGame_idGame_CommentDTO_boolAddCommentToGame()
        {
            var comment = new CommentViewModel();

            _commentController.CommentToGame(_id,comment);

            Assert.True(_isAddCommentToGame);
        }

        [Fact]
        public void GetAllCommentToGameId_Id_boolGetAllCommentToGameId()
        {
            _commentController.GetAllCommentToGame(_id);

            Assert.True(_isGetAllCommentToGameId);
        }

    }
}
