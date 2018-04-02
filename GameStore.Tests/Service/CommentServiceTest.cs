using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        private static readonly Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        private static readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private static readonly Mock<ILog> log = new Mock<ILog>();
        private static readonly CommentService CommentService = new CommentService(uow.Object, mapper.Object, log.Object);

        private readonly List<Comment> _comment = new List<Comment>();

        public CommentServiceTest()
        {
            uow.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback(() => _comment.Add(It.IsAny<Comment>()));
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
