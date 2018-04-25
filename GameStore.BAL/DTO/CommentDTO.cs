using System;

namespace GameStore.BLL.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        public string Quote { get; set; }

        public Guid GameId { get; set; }

        public GameDTO Game { get; set; }
    }
}
