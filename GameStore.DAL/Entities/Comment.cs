using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Comment: BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        public Game Game { get; set; }
    }
}
