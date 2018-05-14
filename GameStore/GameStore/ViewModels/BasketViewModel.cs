using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class BasketViewModel
    {
        public string GameName { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public decimal Price { get; set; }

    }
}