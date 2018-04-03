using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Exeption;
using GameStore.BAL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GameStore.BAL.Service
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

        public void AddComment(CommentDTO commentDto,Guid? parentCommentId)
        {
            if(parentCommentId != null)
            {
                commentDto.ParentCommentId = parentCommentId;
            }
            commentDto.Id=Guid.NewGuid();
            _unitOfWork.Comments.Create(_mapper.Map<Comment>(commentDto));
            _unitOfWork.Save();

            _log.Info("CommentService - add comment " + commentDto.Id);
        }

        public ICollection<CommentDTO> GetAllComments(Guid id)
        {
            IEnumerable<Comment> listCommentToGame;

            if(_unitOfWork.Games.GetById(id)!=null)
            {
                listCommentToGame = _unitOfWork.Comments.GetAll().ToList().Where(game => game.Id == id).ToList();
                _log.Info("CommentService - return all comment to gameId "+ id);
            }
            else
            {
                throw new EntityNotFound(
                    "CommentService - exception in returning all comment to gameId "+ id + ", such game id did not exist", _log);
            }

            return _mapper.Map<List<CommentDTO>>(listCommentToGame);
        }
    }
}
