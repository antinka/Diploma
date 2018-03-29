﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Game
    {
       [Key]
       public Guid Id { get; set; }
       [Index("Index_Key", 1, IsUnique = true)]
       public string Key { get; set; }
	   public string Name { get; set; }
       public string Description { get; set; }

       public ICollection<Comment> Comments { get; set; }
       public ICollection<Genre> Genres { get; set; }
       public ICollection<PlatformType> PlatformTypes { get; set; }

        public Game()
        { }

        public Game(string Name, string Description)
        {
            Id = Guid.NewGuid();
            Key = Id.ToString();
            this.Name = Name;
            this.Description = Description;
        }

        public Game(string Name, string Description, string Key)
        {
            Id = Guid.NewGuid();
            this.Key = Key;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
