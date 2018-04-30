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
using GameStore.BLL.Enums;
using GameStore.Infastracture;
using Xunit;

namespace GameStore.Tests.Service
{
    public class CommentServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly CommentService _sut;
        private readonly IMapper _mapper;

        private readonly Guid _fakeCommentId;
        private readonly string _gameKey;
        private readonly List<Comment> _fakeComments;
        private readonly Comment _fakeComment;
        private readonly Game _fakeGame;

        public CommentServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            var log = new Mock<ILog>();
            _sut = new CommentService(_uow.Object, _mapper, log.Object);

            _gameKey = Guid.NewGuid().ToString();
            _fakeCommentId = Guid.Empty;

            _fakeGame = new Game()
            {
                Id = Guid.NewGuid(),
                Key = _gameKey
            };

            _fakeComment = new Comment()
            {
                Id = _fakeCommentId,
                Game = _fakeGame,
                Name = "1",
                Body = "1"
            };

            _fakeComments = new List<Comment>
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
        public void GetCommentsByGameKey_NotExistGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(null as IEnumerable<Game>);

            Assert.Throws<EntityNotFound>(() => _sut.GetCommentsByGameKey(_gameKey));
        }

        [Fact]
        public void GetCommentsByGameKey_ExistGameId_ReturnedCommentsByGameKey()
        {
            _uow.Setup(uow => uow.Games.Get(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>() { _fakeGame });
            _uow.Setup(uow => uow.Comments.Get(It.IsAny<Func<Comment, bool>>())).Returns(_fakeComments);

            var resultCommentsByGameId = _sut.GetCommentsByGameKey(_gameKey);

            Assert.True(resultCommentsByGameId.All(x => x.Game.Key == _gameKey));
        }

        [Fact]
        public void GetCommentById_ExistedCommentId_CommentReturned()
        {
            _uow.Setup(uow => uow.Comments.GetById(_fakeCommentId)).Returns(_fakeComment);

            var resultComment = _sut.GetById(_fakeCommentId);

            Assert.True(resultComment.Id == _fakeCommentId);
        }

        [Fact]
        public void GetCommentById_NotExistedGameId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Comments.GetById(_fakeCommentId)).Returns(null as Comment);

            Assert.Throws<EntityNotFound>(() => _sut.GetById(_fakeCommentId));
        }

        [Fact]
        public void DeleteComment_ExistedCommentId_CommentDeleted()
        {
            _uow.Setup(uow => uow.Comments.GetById(_fakeCommentId)).Returns(_fakeComment).Verifiable();

            _sut.Delete(_fakeCommentId);

            _uow.Verify(uow => uow.Comments.Delete(_fakeCommentId), Times.Once);
        }

        [Fact]
        public void DeleteComment_ExistedCommentId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Comments.GetById(_fakeCommentId)).Returns(null as Comment);

            Assert.Throws<EntityNotFound>(() => _sut.Delete(_fakeCommentId));
        }

        [Fact]
        public void Ban_NotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => _sut.Ban(BanPeriod.Week, _fakeCommentId));
        }
    }
}
