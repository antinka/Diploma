using System;

namespace GameStore.BAL.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        public GameDTO Game { get; set; }
    }
}
