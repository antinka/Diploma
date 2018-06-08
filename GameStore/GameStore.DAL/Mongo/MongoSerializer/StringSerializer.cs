using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GameStore.DAL.Mongo.MongoSerializer
{
    public class StringSerializer : SerializerBase<string>
    {
        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();

            if (bsonType == BsonType.Null)
            {
                context.Reader.ReadNull();

                return null;
            }

            if (bsonType == BsonType.Int32)
            {
                return context.Reader.ReadInt32().ToString();
            }

            if (bsonType == BsonType.String)
            {
                return context.Reader.ReadString();
            }

            return null;
        }
    }
}