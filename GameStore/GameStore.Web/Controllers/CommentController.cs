using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IGameService gameService, IMapper mapper, IAuthentication authentication) : base(authentication)
        {
            _commentService = commentService;
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet]
        public ActionResult CommentToGame(string gamekey, Guid gameId, Guid? parentsCommentId, string quote)
        {
            var comment = new CommentViewModel { GameId = gameId, GameKey = gamekey };

            if (parentsCommentId != null)
            {
                comment.ParentCommentId = parentsCommentId.Value;
                comment.ParentCommentBody = _commentService.GetById(parentsCommentId.Value).Body;
            }

            if (!string.IsNullOrEmpty(quote))
            {
                comment.Quote = quote;
            }

            return PartialView(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommentToGame(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComment(_mapper.Map<CommentDTO>(comment));

                return PartialView("CommentAdded");
            }

            return PartialView(comment);
        }

        [HttpPost]
        public ActionResult Delete(Guid? commentId, CommentViewModel comment)
        {
            if (commentId != null)
            {
                var commentDTO = _commentService.GetById(commentId.Value);
                var commentViewModel = _mapper.Map<CommentViewModel>(commentDTO);

                return View(commentViewModel);
            }
            _commentService.Delete(comment.Id);

            return RedirectToAction("GetAllCommentToGame", "Comment", new {gamekey = comment.GameKey});
        }

        [HttpGet]
        public ActionResult Ban(BanPeriod? period)
        {
            var userId = Guid.Empty;

            if (period != null)
            {
                _commentService.Ban(period.Value, userId);

                return RedirectToAction("FilteredGames", "Game");
            }

            return PartialView();
        }
    }
}