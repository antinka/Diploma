using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly CommentService _sut;
        private readonly Guid _id;
        private readonly List<Comment> _fakeComment;
        private readonly Game _fakeGame;
        public CommentServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var log = new Mock<ILog>();
            _sut = new CommentService(_uow.Object, mapper.Object, log.Object);
            _id = Guid.NewGuid();
            _fakeComment = new List<Comment>
            {
                new Comment(),
                new Comment()
            };
            _fakeGame = new Game();
        }

        [Fact]
        public void AddComment_Comment_CommentAdded()
        {
            _uow.Setup(x => x.Comments.Create(It.IsAny<Comment>()));

            _sut.AddComment(new CommentDTO());

            _uow.Verify(repository => repository.Comments.Create(It.IsAny<Comment>()),Times.Once);
        }

        [Fact]
        public void GetCommentsByGameId_NotExistGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(repository => repository.Games.GetById(_id)).Throws(new EntityNotFound("NotExistingGameId"));

            Assert.Throws<EntityNotFound>(() => _sut.GetCommentsByGameId(_id));
        }

        [Fact]
        public void GetCommentsByGameId_ExistGameId_GetedCommentsByGameId()
        {
            _uow.Setup(repository => repository.Games.GetById(_id)).Returns(_fakeGame);
            _uow.Setup(repository => repository.Comments.GetAll()).Returns(_fakeComment);

            var commentsByGameId = _sut.GetCommentsByGameId(_id);

            Assert.NotNull(commentsByGameId);
        }
    }
}
