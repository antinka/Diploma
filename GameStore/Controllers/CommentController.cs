using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Filters;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
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
			//todo path gomeKey with viewModel
            TempData["gamekey"] = gamekey;
            var comments = _mapper.Map<List<CommentViewModel>>(_commentService.GetCommentsByGameKey(gamekey));
			//todo same, don't use ViewBag
            ViewBag.gameId = _gameService.GetByKey(gamekey).Id;

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
            var comment = new CommentViewModel {GameId = gameId};

            return PartialView(comment);
        }

        [HttpPost]
        public ActionResult CommentToGame(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComment(_mapper.Map<CommentDTO>(comment));
				//todo temp data
                var gamekey = TempData["gamekey"];

                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = gamekey });
            }
			
            return PartialView(comment);
        }

		//todo why you need this?
        [HttpGet]
        public ActionResult Delete(Guid commentId)
        {
            ViewBag.commentId = commentId;

            return View();
        }


		//todo remove "sure" pls
		[HttpPost]
        public ActionResult Delete(Guid commentId, string sure)
        {
			//todo use viewmodel
            ViewBag.commentId = commentId;
            var gamekey = TempData["gamekey"];

            if (sure == "Yes")
            {
                _commentService.Delete(commentId);

                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = gamekey });
            }

            return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = gamekey });
        }

		//todo how you can ban comment? or why you need this action?
        [HttpGet]
        public ActionResult Ban(Guid commentId)
        {
            ViewBag.commentId = commentId;

            return PartialView();
        }

		//todo why you need to ban commen?
        [HttpPost]
        public ActionResult Ban(Guid commentId, BanPeriod period)
        {
            _commentService.Ban(period, commentId);

            return RedirectToAction("GetAllGames", "Game");
        }
    }
}
