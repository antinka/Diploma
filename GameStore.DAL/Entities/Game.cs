﻿using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Game: BaseEntity
    {
       [Index("Index_Key", 1, IsUnique = true)]
       [MaxLength(450)]
       public string Key { get; set; }
	   public string Name { get; set; }
       public string Description { get; set; }

       public virtual ICollection<Comment> Comments { get; set; }

       public virtual ICollection<Genre> Genres { get; set; }

       public virtual ICollection<PlatformType> PlatformTypes { get; set; }

    }
}