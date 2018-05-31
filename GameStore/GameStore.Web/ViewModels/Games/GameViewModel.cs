using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GameStore.Web.ViewModels.Games
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9_-]{3,200}", ErrorMessage = "Key cannot be longer than 200 characters and less than 3 characters and could contains only A-Za-z0-9")]
        public string Key { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name cannot be longer than 200 characters and less than 3 characters")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Units in stock")]
        [Range(0, 100000)]
        public short UnitsInStock { get; set; }

        public bool Discountinues { get; set; }

        public DateTime PublishDate { get; set; }

        public int Views { get; set; }

        public Guid? PublisherId { get; set; }

        public PublisherViewModel Publisher { get; set; }

        public SelectList PublisherList { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

        public ICollection<Guid> GenresId { get; set; }

        public SelectList GenreList { get; set; }

        [Display(Name = "Platform types")]
        public ICollection<PlatformTypeViewModel> PlatformTypes { get; set; }

        public ICollection<Guid> PlatformTypesId { get; set; }

        public SelectList PlatformTypeList { get; set; }

        public IEnumerable<CheckBox> ListGenres { get; set; }

        public IEnumerable<CheckBox> SelectedGenres { get; set; }

        public ICollection<string> SelectedGenresName { get; set; }

        public IEnumerable<CheckBox> ListPlatformTypes { get; set; }

        public IEnumerable<CheckBox> SelectedPlatformTypes { get; set; }

        public ICollection<string> SelectedPlatformTypesName { get; set; }

    }
}
