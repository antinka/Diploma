using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Units in stock")]
        public short UnitsInStock { get; set; }

        public bool Discountinues { get; set; }

        public Guid? PublisherId { get; set; }

        public PublisherViewModel Publisher { get; set; }

        public SelectList PublisherList { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

        public IEnumerable<Guid> GenresId { get; set; }

        public SelectList GenreList { get; set; }

        [Display(Name = "Platform types")]
        public ICollection<PlatformTypeViewModel> PlatformTypes { get; set; }

        public IEnumerable<Guid> PlatformTypesId { get; set; }

        public SelectList PlatformTypeList { get; set; }

        public IEnumerable<Guid> PlatformTypesIs { get; set; }
    }
}
