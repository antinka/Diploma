using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 200 characters and less than 3 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Message cannot be longer than 200 characters and less than 3 characters")]
        public string Body { get; set; }

        public string Quote { get; set; }

        public Guid? ParentCommentId { get; set; }

        [Display(Name = "Answer to")]
        public string ParentCommentBody { get; set; }

        public Guid GameId { get; set; }

        public string GameKey { get; set; }

        public FilterGameViewModel FilterGame { get; set; }
    }
}
