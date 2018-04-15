using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentDTO commentDto);

        IEnumerable<CommentDTO> GetCommentsByGameId(Guid id);
    }
}
