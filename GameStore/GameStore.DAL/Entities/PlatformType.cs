using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Mongo.MongoSerializer;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class PlatformType : BaseEntity
    {
        [Index("PlatformType_Index_Name", 1, IsUnique = true)]
        [MaxLength(450)]
        public string NameEn { get; set; }

        [MaxLength(450)]
        public string NameRu { get; set; }

        [BsonSerializer(typeof(ListGameSerializer))]
        public virtual ICollection<Game> Games { get; set; }
    }
}
