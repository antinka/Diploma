using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.DAL.Mongo.MongoEntities;
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

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        [BsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
