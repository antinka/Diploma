using GameStore.App_Start;
using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Controllers;
using GameStore.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest
    {
        static Mock<ICommentService> gameRepo = new Mock<ICommentService>();
        public Guid id = Guid.NewGuid();
        bool boolAddCommentToGame = false;
        bool boolGetAllCommentToGameId = false;
        CommentController commentController = new CommentController(gameRepo.Object);

        public CommentControllerTest()
        {
            AutoMapper.Mapper.Reset();
            gameRepo.Setup(x => x.AddCommentToGame(It.IsAny<CommentDTO>(),null)).Callback(() => boolAddCommentToGame=true);
            gameRepo.Setup(x => x.GetAllCommentToGameId(It.IsAny<Guid>())).Callback(() => boolGetAllCommentToGameId = true);
        }

        [Fact]
        public void AddCommentToGame_idGame_CommentDTO_boolAddCommentToGame()
        {
            CommentViewModel comment = new CommentViewModel();

            commentController.CommentToGame(id,comment);

            Assert.True(boolAddCommentToGame);
        }

        [Fact]
        public void GetAllCommentToGameId_Id_boolGetAllCommentToGameId()
        {
            commentController.GetAllCommentToGame(id);

            Assert.True(boolGetAllCommentToGameId);
        }

    }
}
