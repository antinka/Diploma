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
        private readonly ICommentService commentService;


        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult CommentToGame(Guid gamekey,CommentViewModel comment)
        {
            comment.GameId = gamekey;
            commentService.AddCommentToGame(AutoMapper.Mapper.Map<CommentViewModel, CommentDTO>(comment),null);
            return Json("CommentToGame", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult CommentToComment(Guid idGame, CommentViewModel comment,Guid parentCommentId)
        {
            comment.GameId = idGame;
            commentService.AddCommentToGame(AutoMapper.Mapper.Map<CommentViewModel, CommentDTO>(comment), parentCommentId);
            return Json("CommentToComment", JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult GetAllCommentToGame(Guid idGame)
        {
           List<CommentViewModel> comments= AutoMapper.Mapper.Map <IEnumerable<CommentDTO>, List<CommentViewModel >>(commentService.GetAllCommentToGameId(idGame));
            return Json("CommentToComment", JsonRequestBehavior.AllowGet);
        }
    }
}