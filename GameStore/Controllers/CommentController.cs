using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Controllers
{
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
        public ActionResult CommentToGame(Guid gamekey, CommentViewModel comment)
        {
            comment.GameId = gamekey;
            _commentService.AddComment(_mapper.Map<CommentViewModel, CommentDTO>(comment), null);
            return Json("CommentToGame", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult CommentToComment(Guid idGame, CommentViewModel comment, Guid parentCommentId)
        {
            comment.GameId = idGame;
            _commentService.AddComment(_mapper.Map<CommentViewModel, CommentDTO>(comment), parentCommentId);
            return Json("CommentToComment", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult GetAllCommentToGame(Guid idGame)
        {
            var comments = _mapper.Map<IEnumerable<CommentDTO>, List<CommentViewModel>>(_commentService.GetAllComments(idGame));
            return Json("CommentToComment", JsonRequestBehavior.AllowGet);
        }
    }
}