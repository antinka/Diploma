using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        [BsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
