using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public bool Adulthood { get; set; }

        public bool IsWoman { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsBaned { get; set; }

        public DateTime? StartDateBaned { get; set; }

        public DateTime? EndDateBaned { get; set; }

        [BsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }

        [ForeignKey("Publisher")]
        public Guid? PublisherId { get; set; }

        [BsonIgnore]
        public virtual Publisher Publisher { get; set; }
    }
}
