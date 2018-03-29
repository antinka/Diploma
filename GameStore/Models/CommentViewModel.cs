﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class CommentViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public Guid? ParentCommentId { get; set; }
        [ForeignKey("Game")]
        public Guid GameId { get; set; }
        public GameViewModel Game { get; set; }

        public CommentViewModel()
        { }

        public CommentViewModel(GameViewModel Game, string Name, string Body)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.Game = Game;
            this.Body = Body;
        }

        public CommentViewModel(GameViewModel Game, string Name, string Body, Guid ParentCommentId)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.Game = Game;
            this.Body = Body;
            this.ParentCommentId = ParentCommentId;
        }
    }
}
