using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public bool IsDelete { get; set; }
        public Guid? ParentCommentId { get; set; }
        [ForeignKey("Game")]
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        public Comment()
        { }

        public Comment(Game Game, string Name, string Body)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.Game = Game;
            this.Body = Body;
        }

        public Comment(Game Game, string Name, string Body, Guid ParentCommentId)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.Game = Game;
            this.Body = Body;
            this.ParentCommentId = ParentCommentId;
        }
    }
}
