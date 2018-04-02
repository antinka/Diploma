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
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
        private static readonly Mock<ICommentService> GameRepo = new Mock<ICommentService>();
        public Guid Id = Guid.NewGuid();
        private bool _isAddCommentToGame = false;
        private bool _isGetAllCommentToGameId = false;
        private readonly CommentController _commentController = new CommentController(GameRepo.Object, Mapper.Object);

        public CommentControllerTest()
        {
            AutoMapper.Mapper.Reset();
            GameRepo.Setup(x => x.AddComment(It.IsAny<CommentDTO>(),null)).Callback(() => _isAddCommentToGame=true);
            GameRepo.Setup(x => x.GetAllComments(It.IsAny<Guid>())).Callback(() => _isGetAllCommentToGameId = true);
        }

        [Fact]
        public void AddCommentToGame_idGame_CommentDTO_boolAddCommentToGame()
        {
            var comment = new CommentViewModel();

            _commentController.CommentToGame(Id,comment);

            Assert.True(_isAddCommentToGame);
        }

        [Fact]
        public void GetAllCommentToGameId_Id_boolGetAllCommentToGameId()
        {
            _commentController.GetAllCommentToGame(Id);

            Assert.True(_isGetAllCommentToGameId);
        }

    }
}
