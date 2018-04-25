using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Filters;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IGameService gameService, IMapper mapper)
        {
            _commentService = commentService;
            _gameService = gameService;
            _mapper = mapper;
        }

        public ActionResult GetAllCommentToGame(string gamekey)
        {
            var comments = _mapper.Map<List<CommentViewModel>>(_commentService.GetCommentsByGameKey(gamekey));

            if (!comments.Any())
            {
                comments = new List<CommentViewModel>()
               {
                   new CommentViewModel()
                   {
                       GameId = _gameService.GetByKey(gamekey).Id,
                       GameKey = gamekey
                   }
               };
            }
            else
            {
                foreach (var comment in comments)
                {
                    comment.GameKey = gamekey;
                }
            }

            return View(comments);
        }

        public ActionResult CommentToGameForParent(Guid gameId, Guid parentsCommentId)
        {
            var comment = new CommentViewModel
            {
                GameId = gameId,
                ParentCommentId = parentsCommentId
            };

            return PartialView(comment);
        }

        public ActionResult CommentToGameWithQuote(Guid gameId, string quote)
        {
            var comment = new CommentViewModel
            {
                GameId = gameId,
                Quote = quote
            };

            return PartialView(comment);
        }

        //todo Why you need this?
        [HttpGet]
        public ActionResult CommentToGame(Guid gameId)
        {
            var comment = new CommentViewModel { GameId = gameId };

            return PartialView(comment);
        }

        [HttpPost]
        public ActionResult CommentToGame(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComment(_mapper.Map<CommentDTO>(comment));

                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = comment.GameKey });
            }

            return PartialView(comment);
        }

        //todo why you need this?
        [HttpGet]
        public ActionResult Delete(Guid commentId)
        {
            var commentDTO = _commentService.GetById(commentId);
            var commentViewModel = _mapper.Map<CommentViewModel>(commentDTO);

            return View(commentViewModel);
        }

        [HttpPost]
        public ActionResult Delete(CommentViewModel comment)
        {
            _commentService.Delete(comment.Id);

            return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = comment.GameKey });
        }

        public ActionResult Ban(Guid userId, BanPeriod? period)
        {
            if (period != null)
            {
                _commentService.Ban(period.Value, userId);

                return RedirectToAction("GetAllGames", "Game");
            }
            return PartialView();
        }
    }
}
