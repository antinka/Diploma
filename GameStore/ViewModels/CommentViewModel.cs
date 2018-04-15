using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        public string Quote { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Guid GameId { get; set; }

        public GameViewModel Game { get; set; }
    }
}
