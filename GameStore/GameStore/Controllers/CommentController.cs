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
            TempData["gamekey"] = gamekey;
            var comments = _mapper.Map<List<CommentViewModel>>(_commentService.GetCommentsByGameKey(gamekey));
            ViewBag.gameId = _gameService.GetByKey(gamekey).Id;

            return View(comments);
        }

        public ActionResult CommentToGameForParent(Guid gameId, Guid parentsCommentId)
        {
            CommentViewModel comment = new CommentViewModel();
            comment.GameId = gameId;
            comment.ParentCommentId = parentsCommentId;

            return PartialView(comment); 
        }

        public ActionResult CommentToGameWithQuote(Guid gameId, string quote)
        {
            CommentViewModel comment = new CommentViewModel();
            comment.GameId = gameId;
            comment.Quote = quote;

            return PartialView(comment);
        }

        [HttpGet]
        public ActionResult CommentToGame(Guid gameId)
        {
            CommentViewModel comment = new CommentViewModel();
            comment.GameId = gameId;

            return PartialView(comment);
        }

        [HttpPost]
        public ActionResult CommentToGame(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComment(_mapper.Map<CommentDTO>(comment));
                var gamekey = TempData["gamekey"];

                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = gamekey });
            }
            else
            {
                return PartialView(comment);
            }
        }

        [HttpGet]
        public ActionResult Delete(Guid commentId)
        {
            ViewBag.commentId = commentId;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid commentId, string sure)
        {
            ViewBag.commentId = commentId;
            var gamekey = TempData["gamekey"];

            if (sure == "Yes")
            {
                _commentService.Delete(commentId);

                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = gamekey });
            }
            else
            {
                return RedirectToAction("GetAllCommentToGame", "Comment", new { gamekey = gamekey });
            }
        }

        [HttpGet]
        public ActionResult Ban(Guid commentId)
        {
            ViewBag.commentId = commentId;

            return PartialView();
        }

        [HttpPost]
        public ActionResult Ban(Guid commentId, BanPeriod period)
        {
            _commentService.Ban(period, commentId);

            return RedirectToAction("GetAllGames", "Game");
        }
    }
}
