using AutoMapper;
using GameStore;
using GameStore.BAL.DTO;
using GameStore.BAL.Exeption;
using GameStore.BAL.Infastracture;
using GameStore.BAL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameStore.BAL.Service
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork Db { get; set; }
        private readonly ILog _log = LogManager.GetLogger("LOGGER");
        private readonly IMapper _mapper = GameStore.Infastracture.MapperConfigBLL.GetMapper();

        public CommentService(IUnitOfWork uow)
        {
            Db = uow;
        }

        public void AddCommentToGame(CommentDTO commentDto,Guid? parentCommentId)
        {
            if(parentCommentId != null)
            {
                commentDto.ParentCommentId = parentCommentId;
            }
            Db.Comments.Create(_mapper.Map<CommentDTO, Comment>(commentDto));
            Db.Save();

            _log.Info("CommentService - add comment to game");
        }

        public ICollection<CommentDTO> GetAllCommentToGameId(Guid id)
        {
            IEnumerable<Comment> listCommentToGame = new List<Comment>();
            if(Db.Games.Get(id)!=null)
            {
                listCommentToGame = Db.Comments.GetAll().ToList().Where(game => game.Id == id).ToList();
                _log.Info("CommentService - return all comment to gameId");
            }
            else
            {
                throw new GameStoreExeption(
                    "CommentService - exception in returning all comment to gameId, such game id did not exist");
            }

            return _mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(listCommentToGame);
        }
    }
}
