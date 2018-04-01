using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BAL.Interfaces
{
    public interface ICommentService
    {
        void AddCommentToGame(CommentDTO commentDto, Guid? parentCommentId);
        ICollection<CommentDTO> GetAllCommentToGameId(Guid id);
    }
}
