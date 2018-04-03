using GameStore.BAL.DTO;
using GameStore.BAL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System;
using AutoMapper;
using GameStore.BAL;
using log4net;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        private static readonly Mock<IUnitOfWork> GameRepo = new Mock<IUnitOfWork>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
        private static readonly Mock<ILog> Log = new Mock<ILog>();
        private static readonly CommentService CommentService = new CommentService(GameRepo.Object, Mapper.Object, Log.Object);

        private readonly List<Comment> _comment = new List<Comment>();

        public CommentServiceTest()
        {
            GameRepo.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback(() => _comment.Add(It.IsAny<Comment>()));
        }

        [Fact]
        public void AddCommentToGame_add1Comment_1CommentInList()
         {
            var commentDto = new CommentDTO();

            CommentService.AddComment(commentDto, null);
            var result = _comment.Count();
            Assert.Equal(1, result);
         }
    }
}
