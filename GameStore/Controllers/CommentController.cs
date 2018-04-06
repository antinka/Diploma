using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using GameStore.Filters;

namespace GameStore.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult AddCommentToGame(Guid gamekey, CommentViewModel comment)
        {
            comment.GameId = gamekey;

            _commentService.AddComment(_mapper.Map<CommentDTO>(comment));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult GetAllCommentToGame(Guid gamekey)
        {
             var comments = _mapper.Map<IEnumerable<CommentDTO>>(_commentService.GetCommentsByGameId(gamekey));

            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}