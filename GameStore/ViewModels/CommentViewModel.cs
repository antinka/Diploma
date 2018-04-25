using System;

namespace GameStore.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Guid GameId { get; set; }
    }
}
