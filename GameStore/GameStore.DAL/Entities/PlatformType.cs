using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class PlatformType : BaseEntity
    {
        [Index("PlatformType_Index_Name", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Name { get; set; }

        [BsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
