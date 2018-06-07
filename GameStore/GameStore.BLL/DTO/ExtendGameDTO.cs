using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class ExtendGameDTO
    {
        public Guid Id { get; set; }

        public bool IsDelete { get; set; }

        public string Key { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionRu { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discountinues { get; set; }

        public DateTime PublishDate { get; set; }

        public int Views { get; set; }

        public Guid? PublisherId { get; set; }

        public PublisherDTO Publisher { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }

        public ICollection<GenreDTO> Genres { get; set; }

        public ICollection<PlatformTypeDTO> PlatformTypes { get; set; }

        public ICollection<string> SelectedGenresName { get; set; }

        public ICollection<string> SelectedPlatformTypesName { get; set; }
    }
}
