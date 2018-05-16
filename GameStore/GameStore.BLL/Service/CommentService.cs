using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Service
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        private const string BodyDeletedComment = "This comment was deleted";

        public CommentService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddComment(CommentDTO commentDto)
        {
            commentDto.Id = Guid.NewGuid();
            var comment = _mapper.Map<Comment>(commentDto);
            _unitOfWork.Comments.Create(comment);
            _unitOfWork.Save();

            _log.Info($"{nameof(CommentService)} - add comment{commentDto.Id}");
        }

        public CommentDTO GetById(Guid id)
        {
            var comment = _unitOfWork.Comments.GetById(id);

            if (comment == null)
            {
                throw new EntityNotFound($"{nameof(CommentService)} - comment with such id {id} did not exist");
            }

            return _mapper.Map<CommentDTO>(comment);
        }

        public void Delete(Guid id)
        {
            var comment = _unitOfWork.Comments.GetById(id);

            if (comment == null)
                throw new EntityNotFound($"{nameof(CommentService)} - attempt to delete not existed comment, id {id}");

            comment.Body = BodyDeletedComment;
            _unitOfWork.Comments.Update(comment);

            _unitOfWork.Save();

            _log.Info($"{nameof(CommentService)} - delete comment{id}");
        }

        public BanPeriod Ban(BanPeriod period, Guid userId)
        {
            return period;
        }

        public IEnumerable<CommentDTO> GetCommentsByGameKey(string gameKey)
        {
            var game = _unitOfWork.Games.Get(g => g.Key == gameKey).FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFound(
                    $"{nameof(CommentService)}- exception in returning all comment to gameKey {gameKey} such game key did not exist");
            }

            var listCommentToGame = _unitOfWork.Comments.Get(g => g.GameId == game.Id);

            return _mapper.Map<IEnumerable<CommentDTO>>(listCommentToGame);
        }
    }
}