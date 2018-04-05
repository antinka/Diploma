using AutoMapper;
using GameStore.BLL.DTO;
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

       public CommentServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var log = new Mock<ILog>();
            _sut = new CommentService(_uow.Object, mapper.Object, log.Object);
        }

        [Fact]
        public void AddComment_Comment_CommentAdded()
        {
            _uow.Setup(x => x.Comments.Create(It.IsAny<Comment>()));

            _sut.AddComment(new CommentDTO());

            _uow.Verify(x => x.Comments.Create(It.IsAny<Comment>()),Times.Once);
        }
    }
}
