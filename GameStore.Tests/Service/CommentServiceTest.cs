using GameStore.BAL.DTO;
using GameStore.BAL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System;
using GameStore.BAL;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        static Mock<IUnitOfWorkGeneric> gameRepo = new Mock<IUnitOfWorkGeneric>();
        static CommentService commentService = new CommentService(gameRepo.Object);

        List<Comment> comment = new List<Comment>();
        public Guid id = Guid.NewGuid();

        public CommentServiceTest()
        {
            gameRepo.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback(() => comment.Add(It.IsAny<Comment>()));
        }

        [Fact]
        public void AddCommentToGame_add1Comment_1CommentInList()
         {
            CommentDTO commentDTO = new CommentDTO();

            commentService.AddCommentToGame(commentDTO, null);
            int result = comment.Count();
            Xunit.Assert.Equal(1, result);
         }
    }
}
