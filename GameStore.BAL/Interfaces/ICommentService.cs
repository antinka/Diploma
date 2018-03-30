using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Interfaces
{
    public interface ICommentService
    {
        void AddCommentToGame(CommentDTO commentDto, Guid? parentCommentId);
        ICollection<CommentDTO> GetAllCommentToGameId(Guid id);
    }
}
