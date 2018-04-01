﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.DTO
{
    public class GameDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}