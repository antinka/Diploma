using GameStore.DAL.Mongo.MongoSerializer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoOrder 
    {
        public ObjectId Id { get; set; }

        public int OrderId { get; set; }

        public string OrderDate { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        [BsonSerializer(typeof(StringSerializer))]
        public string ShipAddress { get; set; }

        [BsonSerializer(typeof(StringSerializer))]
        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        [BsonSerializer(typeof(StringSerializer))]
        public string ShipPostalCode { get; set; }

        [BsonSerializer(typeof(StringSerializer))]
        public string ShipCountry { get; set; }
    }
}
