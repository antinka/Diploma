﻿using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using System.Linq;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Service
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddComment(CommentDTO commentDto)
        {
            commentDto.Id = Guid.NewGuid();
			//todo
            _unitOfWork.Comments.Create(_mapper.Map<Comment>(commentDto));
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
			//todo
            else
            {
                return _mapper.Map<CommentDTO>(comment);
            }
        }

        public void Delete(Guid id)
        {
            var comment = _unitOfWork.Comments.GetById(id);

            if (comment == null)
                throw new EntityNotFound($"{nameof(CommentService)} - attempt to delete not existed comment, id {id}");

            _unitOfWork.Comments.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(CommentService)} - delete comment{id}");
        }

		//todo it'll crash
        public void Ban(BanPeriod period, Guid commentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDTO> GetCommentsByGameKey(string gameKey)
        {
            var games = _unitOfWork.Games.Get(g => g.Key == gameKey);

			//todo are you sure that games could be null?
            if (games != null)
            {
                var listCommentToGame = _unitOfWork.Comments.Get(g => g.GameId == games.First().Id);

                return _mapper.Map<IEnumerable<CommentDTO>>(listCommentToGame);
            }
			//todo
            else
            {
                throw new EntityNotFound(
                    $"{nameof(CommentService)}- exception in returning all comment to gameKey {gameKey} such game key did not exist");
            }
        }
    }
}