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
using System.Linq;
using GameStore.Infastracture;
using Xunit;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly CommentService _sut;
        private readonly IMapper _mapper;

        private readonly Guid _id;
        private readonly List<Comment> _fakeComment;
        private readonly Game _fakeGame;

        public CommentServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            var log = new Mock<ILog>();
            _sut = new CommentService(_uow.Object, _mapper, log.Object);

            _id = Guid.NewGuid();

            _fakeGame = new Game()
            {
                Id = _id,
                Key = "123"
            };

            _fakeComment = new List<Comment>
            {
                new Comment()
                {
                    Id = Guid.NewGuid(),
                    Game = _fakeGame,
                    Name = "1",
                    Body = "1"
                },
                new Comment()
                {
                    Id = Guid.NewGuid(),
                    Game = _fakeGame,
                    Name = "2",
                    Body = "2"
                }
            };
        }

        [Fact]
        public void AddComment_Comment_CommentAdded()
        {
            var fakeCommentDTO = new CommentDTO() { Id = Guid.NewGuid(), Name = "3", Body = "3" };
            var fakeComment = _mapper.Map<Comment>(fakeCommentDTO);

            _uow.Setup(x => x.Comments.Create(fakeComment)).Verifiable();

            _sut.AddComment(fakeCommentDTO);

            _uow.Verify(uow => uow.Comments.Create(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public void GetCommentsByGameId_NotExistGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.GetById(_id)).Returns(null as Game);

            Assert.Throws<EntityNotFound>(() => _sut.GetCommentsByGameId(_id));
        }

        [Fact]
        public void GetCommentsByGameId_ExistGameId_ReturnedCommentsByGameId()
        {
            _uow.Setup(uow => uow.Games.GetById(_id)).Returns(_fakeGame);
            _uow.Setup(uow => uow.Comments.Get(It.IsAny<Func<Comment, bool>>())).Returns(_fakeComment);

            var resultCommentsByGameId = _sut.GetCommentsByGameId(_id);

            Assert.True(resultCommentsByGameId.All(x => x.Game.Id == _id));
        }
    }
}
