using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Mongo.MongoSerializer;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class Game : BaseEntity
    {
        [Index("Game_Index_Key", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Key { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionRu { get; set; }

        public decimal Price { get; set; }

        [Column(TypeName = "SMALLINT")]
        public short UnitsInStock { get; set; }

        public bool Discountinues { get; set; }

        public DateTime PublishDate { get; set; }

        public int Views { get; set; }

        [ForeignKey("Publisher")]
        public Guid? PublisherId { get; set; }

        [BsonIgnore]
        public virtual Publisher Publisher { get; set; }

        [BsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }

        [BsonSerializer(typeof(ListGenreSerializer))]
        public virtual ICollection<Genre> Genres { get; set; }

        [BsonSerializer(typeof(ListPlatformTypeSerializer))]
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
    }
}
