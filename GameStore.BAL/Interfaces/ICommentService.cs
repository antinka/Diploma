using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BAL.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentDTO commentDto, Guid? parentCommentId);
        ICollection<CommentDTO> GetAllComments(Guid id);
    }
}
