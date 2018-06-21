using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels.Games
{
    public class FilterGameViewModel
    {
        public Guid Id { get; set; }

        public bool IsDelete { get; set; }

        [Display(Name = "Key", ResourceType = typeof(GlobalRes))]
        [MaxLength(450)]
        public string Key { get; set; }

        [Display(Name = "Name", ResourceType = typeof(GlobalRes))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(GlobalRes))]
        public string Description { get; set; }

        [Display(Name = "Price", ResourceType = typeof(GlobalRes))]
        public decimal Price { get; set; }

        [Display(Name = "UnitsInStock", ResourceType = typeof(GlobalRes))]
        public short UnitsInStock { get; set; }

        [Display(Name = "Discountinues", ResourceType = typeof(GlobalRes))]
        public bool Discountinues { get; set; }

        public DateTime PublishDate { get; set; }

        public int Views { get; set; }

        [Display(Name = "Picture", ResourceType = typeof(GlobalRes))]
        public string ImageName { get; set; }

        public string ImageMimeType { get; set; }

        public Guid? PublisherId { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(GlobalRes))]
        public DetailsPublisherViewModel Publisher { get; set; }

        public SelectList PublisherList { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(GlobalRes))]
        public ICollection<CommentViewModel> Comments { get; set; }

        [Display(Name = "Genres", ResourceType = typeof(GlobalRes))]
        public ICollection<DelailsGenreViewModel> Genres { get; set; }

        public ICollection<Guid> GenresId { get; set; }

        public SelectList GenreList { get; set; }

        [Display(Name = "PlatformTypes", ResourceType = typeof(GlobalRes))]
        public ICollection<DetailsPlatformTypeViewModel> PlatformTypes { get; set; }

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
