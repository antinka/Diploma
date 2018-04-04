using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
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
        public ActionResult CommentToGame(Guid gamekey, CommentViewModel comment, Guid? parentCommentId)
        {
            comment.GameId = gamekey;

            if (parentCommentId!=null)
            {
                comment.ParentCommentId = parentCommentId;
            }

            _commentService.AddComment(_mapper.Map<CommentDTO>(comment));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult GetAllCommentToGame(Guid idGame)
        {
            _mapper.Map<IEnumerable<CommentDTO>>(_commentService.GetCommentsByGameId(idGame));

            return Json("CommentToComment", JsonRequestBehavior.AllowGet);
        }
    }
}