using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.ViewModels;
using Moq;
using System;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest 
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ICommentService> _uow;
        private readonly CommentController _sut;

        private readonly Guid _id;

        public CommentControllerTest()
        {
            Mapper.Reset();

            _id = Guid.NewGuid();
            _mapper = new Mock<IMapper>();
            _uow = new Mock<ICommentService>();
            _sut = new CommentController(_uow.Object, _mapper.Object);
        }

        [Fact]
        public void AddCommentToGame_NewComment_CommentAdded()
        {
            var comment = new CommentViewModel();
            _uow.Setup(x => x.AddComment(It.IsAny<CommentDTO>()));

            _sut.CommentToGame(_id, comment, null);

            _uow.VerifyAll();
        }

        [Fact]
        public void GetAllCommentToGame_GameId_CommentGet()
        {
            _uow.Setup(x => x.GetCommentsByGameId(_id));

            _sut.GetAllCommentToGame(_id);

            _uow.VerifyAll();
        }
    }
}
