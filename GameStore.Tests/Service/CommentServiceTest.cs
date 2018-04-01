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
        private static readonly Mock<IUnitOfWorkGeneric> GameRepo = new Mock<IUnitOfWorkGeneric>();
        private static readonly CommentService CommentService = new CommentService(GameRepo.Object);

        private readonly List<Comment> _comment = new List<Comment>();

        public CommentServiceTest()
        {
            GameRepo.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback(() => _comment.Add(It.IsAny<Comment>()));
        }

        [Fact]
        public void AddCommentToGame_add1Comment_1CommentInList()
         {
            var commentDto = new CommentDTO();

            CommentService.AddCommentToGame(commentDto, null);
            var result = _comment.Count();
            Xunit.Assert.Equal(1, result);
         }
    }
}
