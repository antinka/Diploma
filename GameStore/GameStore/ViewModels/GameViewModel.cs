using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.App_LocalResources;

namespace GameStore.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Key", ResourceType = typeof(GlobalRes))]
        [MaxLength(450)]
        public string Key { get; set; }

        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(GlobalRes))]
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price", ResourceType = typeof(GlobalRes))]
        [Range(0, Int32.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "UnitsInStock", ResourceType = typeof(GlobalRes))]
        [Range(0, Int32.MaxValue)]
        public short UnitsInStock { get; set; }

        [Display(Name = "Discountinues", ResourceType = typeof(GlobalRes))]
        public bool Discountinues { get; set; }

        public Guid? PublisherId { get; set; }

        public PublisherViewModel Publisher { get; set; }

        public SelectList PublisherList { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(GlobalRes))]
        public ICollection<CommentViewModel> Comments { get; set; }

        [Display(Name = "Genres", ResourceType = typeof(GlobalRes))]
        public ICollection<GenreViewModel> Genres { get; set; }

        public ICollection<Guid> GenresId { get; set; }

        public SelectList GenreList { get; set; }

        [Display(Name = "PlatformTypes", ResourceType = typeof(GlobalRes))]
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
