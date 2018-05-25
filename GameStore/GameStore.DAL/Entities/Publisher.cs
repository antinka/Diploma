using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        [Column(TypeName = "nvarchar")]
        [MaxLength(40)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        [BsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
