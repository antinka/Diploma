using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class Shipper
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }
    }
}
