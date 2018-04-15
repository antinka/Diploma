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

        public CommentController(ICommentService commentService, IGameService gameService,  IMapper mapper)
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

        [OutputCache(Duration = 60)]
        [HttpPost]
        public ActionResult AddCommentToGame(Guid gamekey, CommentViewModel comment)
        {
            comment.GameId = gamekey;

            _commentService.AddComment(_mapper.Map<CommentDTO>(comment));

            return PartialView();
        }

        [HttpPost]
        public ActionResult GetAllCommentToGame(Guid gamekey)
        {
             var comments = _mapper.Map<IEnumerable<CommentDTO>>(_commentService.GetCommentsByGameId(gamekey));

            return RedirectToAction("GetAllGames", "Game");
        }
    }
}