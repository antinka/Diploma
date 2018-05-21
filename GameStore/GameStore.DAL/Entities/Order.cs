using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public bool IsPaid { get; set; }

        public string ShipperId { get; set; }

        [BsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
