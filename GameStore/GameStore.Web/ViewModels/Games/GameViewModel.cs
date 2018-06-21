using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels.Games
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [RegularExpression(@"^[A-Za-z0-9_-]{3,100}", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "KeyRegexExpression")]
        [Display(Name = "Key", ResourceType = typeof(GlobalRes))]
        [MaxLength(450)]
        public string Key { get; set; }

        [Display(Name = "NameEn", ResourceType = typeof(GlobalRes))]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [StringLength(200, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "NameExpression3_200")]
        public string NameEn { get; set; }

        [Display(Name = "NameRu", ResourceType = typeof(GlobalRes))]
        public string NameRu { get; set; }

        [Display(Name = "DescriptionEn", ResourceType = typeof(GlobalRes))]
        public string DescriptionEn { get; set; }

        [Display(Name = "DescriptionRu", ResourceType = typeof(GlobalRes))]
        public string DescriptionRu { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [Display(Name = "Price", ResourceType = typeof(GlobalRes))]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [Display(Name = "UnitsInStock", ResourceType = typeof(GlobalRes))]
        [Range(0, 100000)]
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