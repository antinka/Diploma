﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.ViewModels
{
    public class PlatformTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 200 characters and less than 3 characters")]
        public string Name { get; set; }

        public ICollection<GameViewModel> Games { get; set; }
    }
}
