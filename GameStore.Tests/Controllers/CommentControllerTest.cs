﻿using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.Infastracture;
using GameStore.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class CommentControllerTest
    {
        private readonly Mock<ICommentService> _commentService;
        private readonly IMapper _mapper;
        private readonly CommentController _sut;

        private readonly List<CommentDTO> _fakeComments;
        private readonly string _gameKey;

        public CommentControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _commentService = new Mock<ICommentService>();
            _sut = new CommentController(_commentService.Object, _mapper);

            _gameKey = Guid.NewGuid().ToString();

            _fakeComments = new List<CommentDTO>
            {
                new CommentDTO
                {
                    Body = "body1",
                    Game = new GameDTO(),
                    Id = Guid.NewGuid(),
                    Name = "name1",
                    ParentCommentId = null
                },
                new CommentDTO
                {
                    Body = "body2",
                    Game = new GameDTO(),
                    Id = Guid.NewGuid(),
                    Name = "name2",
                    ParentCommentId = null
                }
            };
        }

        [Fact]
        public void AddCommentToGame_ValidComment_HttpStatusCodeOK()
        {
            _commentService.Setup(service => service.AddComment(It.IsAny<CommentDTO>()));

            var httpStatusCodeResult = _sut.AddCommentToGame( new CommentViewModel()) as HttpStatusCodeResult;

            Assert.Equal(200, httpStatusCodeResult.StatusCode);
        }

        [Fact]
        public void GetAllCommentToGame_GameKey_ReturnedGames()
        {
            _commentService.Setup(service => service.GetCommentsByGameKey(_gameKey)).Returns(_fakeComments);

            var commentResult = _sut.GetAllCommentToGame(_gameKey) as JsonResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(commentResult.Data);

            Assert.Equal(_fakeComments.Count, data.Count);
        }
    }
}
