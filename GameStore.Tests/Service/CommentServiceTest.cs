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
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILog> log;
        private readonly CommentService _sut;

        public CommentServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            log = new Mock<ILog>();
            _sut = new CommentService(_uow.Object, _mapper.Object, log.Object);
        }

        [Fact]
        public void AddComment_Comment_VerifyAll()
        {
            var commentDto = new CommentDTO();
            _uow.Setup(x => x.Comments.Create(It.IsAny<Comment>()));

            _sut.AddComment(commentDto);

            _uow.VerifyAll();
        }
    }
}
