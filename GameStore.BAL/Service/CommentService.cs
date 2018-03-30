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
        IUnitOfWork db { get; set; }
        ILog log = LogManager.GetLogger("LOGGER");
        IMapper mapper = GameStore.Infastracture.MapperConfigBLL.GetMapper();

        public CommentService(IUnitOfWork uow)
        {
            db = uow;
        }

        public void AddCommentToGame(CommentDTO commentDTO,Guid? parentCommentId)
        {
            if(parentCommentId != null)
            {
                commentDTO.ParentCommentId = parentCommentId;
            }
            db.Comments.Create(mapper.Map<CommentDTO, Comment>(commentDTO));
            db.Save();
            log.Info("CommentService - add comment to game");
        }

        public ICollection<CommentDTO> GetAllCommentToGameId(Guid id)
        {
            IEnumerable<Comment> list = new List<Comment>();
            if(db.Games.Get(id)!=null)
            {
                list = from c in db.Comments.GetAll()
                       where c.GameId == id
                       select c;
                log.Info("CommentService - return all comment to gameId");
            }
            else
            {
                throw new GameStoreExeption("CommentService - exception in returning all comment to gameId, such game id did not exist");

            }
            return mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(list);
        }
    }
}
