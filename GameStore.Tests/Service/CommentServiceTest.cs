using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly CommentService _sut;
        private readonly Guid _id;
        private readonly List<Comment> _faceComment;
        private readonly Game _faceGame;
        public CommentServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var log = new Mock<ILog>();
            _sut = new CommentService(_uow.Object, mapper.Object, log.Object);
            _id = Guid.NewGuid();
            _faceComment = new List<Comment>
            {
                new Comment(),
                new Comment()
            };
            _faceGame = new Game();
        }

        [Fact]
        public void AddComment_Comment_CommentAdded()
        {
            _uow.Setup(x => x.Comments.Create(It.IsAny<Comment>()));

            _sut.AddComment(new CommentDTO());

            _uow.Verify(x => x.Comments.Create(It.IsAny<Comment>()),Times.Once);
        }

        [Fact]
        public void GetCommentsByGameId_NotExistGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(x => x.Games.GetById(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.GetCommentsByGameId(_id));
        }

        [Fact]
        public void GetCommentsByGameId_ExistGameId_GetedCommentsByGameId()
        {
            _uow.Setup(x => x.Games.GetById(_id)).Returns(_faceGame);
            _uow.Setup(x => x.Comments.GetAll()).Returns(_faceComment);

            var commentsByGameId = _sut.GetCommentsByGameId(_id);

            Assert.NotNull(commentsByGameId);
        }
    }
}
