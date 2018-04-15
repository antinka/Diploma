using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentDTO commentDto);

        CommentDTO GetById(Guid id);

        void Delete(Guid id);

        void Ban(BanPeriod period, Guid commentId);

        IEnumerable<CommentDTO> GetCommentsByGameKey(string gameKey);
    }
}
