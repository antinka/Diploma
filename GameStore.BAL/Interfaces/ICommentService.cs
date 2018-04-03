using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentDTO commentDto);

        ICollection<CommentDTO> GetAllComments(Guid id);
    }
}
