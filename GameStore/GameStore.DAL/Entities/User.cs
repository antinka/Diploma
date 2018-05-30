using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsBaned { get; set; }

        public DateTime? StartDateBaned { get; set; }

        public DateTime? EndDateBaned { get; set; }

        [BsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
