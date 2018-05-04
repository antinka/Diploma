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
    }
}
