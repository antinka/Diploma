﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Games;
using Moq;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest
    {
        private readonly Mock<ICommentService> _commentService;
        private readonly IMapper _mapper;
        private readonly CommentController _sut;

        private readonly Guid _fakeCommentId, _fakeGameId;
        private readonly string _fakeGameKey;
        private readonly List<CommentDTO> _fakeComment;

        public CommentControllerTest()
        {
            _commentService = new Mock<ICommentService>();
            var gameService = new Mock<IGameService>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new CommentController(_commentService.Object, gameService.Object, _mapper, null);

            _fakeCommentId = Guid.NewGuid();
            _fakeGameId = Guid.NewGuid();
            _fakeGameKey = _fakeCommentId.ToString();

            _fakeComment = new List<CommentDTO>()
            {
                new CommentDTO()
            };
        }

        [Fact]
        public void GetAllCommentToGame_GameKey_GetCommentsByGameKeyCalled()
        {
            _commentService.Setup(service => service.GetCommentsByGameKey(_fakeGameKey)).Returns(_fakeComment);

            _sut.GetAllCommentToGame(_fakeGameKey);

            _commentService.Verify(s => s.GetCommentsByGameKey(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteComment_CommentViewModel_RedirectToActionResult()
        {
            _commentService.Setup(service => service.Delete(It.IsAny<Guid>()));

            var res = _sut.Delete(null, new CommentViewModel()) as RedirectToRouteResult;

            Assert.Equal("Comment", res.RouteValues["controller"]);
            Assert.Equal("GetAllCommentToGame", res.RouteValues["action"]);
        }

        [Fact]
        public void DeleteComment_CommentId_ReturnPartialViewResult()
        {
            var fakeCommentId = Guid.NewGuid();

            var res = _sut.Delete(fakeCommentId, null);

            Assert.Equal(typeof(PartialViewResult), res.GetType());
        }

        [Fact]
        public void BanComment_UserIdAndPeriod_RedirectToActionResult()
        {
            _commentService.Setup(service => service.Ban(It.IsAny<BanPeriod>(), It.IsAny<Guid>()));

            var res = _sut.Ban(BanPeriod.Day, _fakeGameKey) as RedirectToRouteResult;
        
            Assert.Equal("Comment", res.RouteValues["controller"]);
            Assert.Equal("GetAllCommentToGame", res.RouteValues["action"]);
        }

        [Fact]
        public void BanComment_UserId_ReturnPartialViewResult()
        {
            _commentService.Setup(service => service.Ban(It.IsAny<BanPeriod>(), It.IsAny<Guid>()));

            var res = _sut.Ban(null, _fakeGameKey);

            Assert.IsType<PartialViewResult>(res);
        }

        [Fact]
        public void CommentToGame_ValidComment_RedirectToActionResult()
        {
            var fakeCommentViewModel = new CommentViewModel()
            {
                Name = "test",
                Body = "test",
                FilterGame = new FilterGameViewModel()
            };
            var fakeCommentDTO = _mapper.Map<CommentDTO>(fakeCommentViewModel);

            _commentService.Setup(service => service.AddComment(fakeCommentDTO));

            var res = _sut.CommentToGame(fakeCommentViewModel);

            Assert.IsType<PartialViewResult>(res);
        }

        [Fact]
        public void CommentToGame_GamekeyGameId_ReturnedPartialView()
        {
            var res = _sut.CommentToGame(_fakeGameKey, _fakeGameId, null, null);

            Assert.IsType<PartialViewResult>(res);
        }

        [Fact]
        public void CommentToGame_GameKeyAndIdAndQuoteString_ReturnedView()
        {
            var res = _sut.CommentToGame(_fakeGameKey, _fakeGameId, null, "quote");

            Assert.IsType<PartialViewResult>(res);
        }
    }
}
