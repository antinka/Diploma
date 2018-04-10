using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        public ActionResult CommentToGame(Guid gameId, string quote, Guid parentsCommentId)
        {
            CommentViewModel comment = new CommentViewModel();
            comment.GameId = gameId;
            comment.Quote = quote;
            comment.ParentCommentId = parentsCommentId;

            return PartialView(comment);
        }

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult CommentToGame(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComment(_mapper.Map<CommentDTO>(comment));
                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = comment.Game.Key });
            }

            return PartialView(comment);
        }

        public ActionResult GetAllCommentToGame(string gamekey)
        {
             var comments = _mapper.Map<IEnumerable<CommentViewModel>>(_commentService.GetCommentsByGameKey(gamekey));

           return View(comments);
            //return comments.Count().ToString();
        }
    }
}