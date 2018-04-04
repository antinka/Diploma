﻿using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;


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
            _unitOfWork.Comments.Create(_mapper.Map<Comment>(commentDto));
            _unitOfWork.Save();

            _log.Info($"{nameof(CommentService)} - add comment{commentDto.Id}");
        }

        public IEnumerable<CommentDTO> GetCommentsByGameId(Guid id)
        {
            IEnumerable<Comment> listCommentToGame;
            var games = _unitOfWork.Games.GetById(id);

            if (games != null)
            {
                listCommentToGame = _unitOfWork.Comments.GetAll().Where(game => game.Id == id);
            }
            else
            {
                throw new EntityNotFound(
                    $"{nameof(CommentService)}- exception in returning all comment to gameId {id} such game id did not exist");
            }

            return _mapper.Map<IEnumerable<CommentDTO>>(listCommentToGame);
        }
    }
}
