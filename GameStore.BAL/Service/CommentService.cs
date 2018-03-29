using AutoMapper;
using GameStore;
using GameStore.BAL.DTO;
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
        //  IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
        //   private readonly IMapper mapper;
        
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
            db.Comments.Create(Mapper.Map<CommentDTO, Comment>(commentDTO));
            db.Save();
            log.Info("CommentService - add comment to game");
        }

        public ICollection<CommentDTO> GetAllCommentToGameId(Guid id)
        {
            IEnumerable<Comment> list = new List<Comment>();
            try
            {
                list = from c in db.Comments.GetAll()
                       where c.GameId == id
                       select c;
                log.Info("CommentService - return all comment to gameId");
            }
            catch(Exception ex)
            {
                 log.Error("CommentService - exception in returning all comment to gameId - "+ ex.Message);
            }
          
            return Mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(list);
        }
    }
}
