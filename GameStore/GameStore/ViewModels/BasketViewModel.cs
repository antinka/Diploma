using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class BasketViewModel
    {
        [Required]
        public Guid GameId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Range(1, Int32.MaxValue)]
        public short Quantity { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }
    }
}