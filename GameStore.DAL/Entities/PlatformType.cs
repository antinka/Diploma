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
    public class PlatformType: BaseEntity
    {
        [Index("Index_Type", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
