using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Controllers;
using GameStore.Models;
using Moq;
using System;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest :IDisposable
    {
        private static readonly Mock<ICommentService> GameRepo = new Mock<ICommentService>();
        public Guid Id = Guid.NewGuid();
        private bool _boolAddCommentToGame = false;
        private bool _boolGetAllCommentToGameId = false;
        private readonly CommentController _commentController = new CommentController(GameRepo.Object);

        public CommentControllerTest()
        {
            AutoMapper.Mapper.Reset();
            GameRepo.Setup(x => x.AddCommentToGame(It.IsAny<CommentDTO>(),null)).Callback(() => _boolAddCommentToGame=true);
            GameRepo.Setup(x => x.GetAllCommentToGameId(It.IsAny<Guid>())).Callback(() => _boolGetAllCommentToGameId = true);
        }

        [Fact]
        public void AddCommentToGame_idGame_CommentDTO_boolAddCommentToGame()
        {
            var comment = new CommentViewModel();

            _commentController.CommentToGame(Id,comment);

            Assert.True(_boolAddCommentToGame);
        }

        public void Dispose()
        {
            ((IDisposable)_commentController).Dispose();
        }

        [Fact]
        public void GetAllCommentToGameId_Id_boolGetAllCommentToGameId()
        {
            _commentController.GetAllCommentToGame(Id);

            Assert.True(_boolGetAllCommentToGameId);
        }

    }
}
