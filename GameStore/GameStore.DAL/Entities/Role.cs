using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        [BsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
