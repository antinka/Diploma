using GameStore.DAL.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;

namespace GameStore.DAL.Mongo.MongoSerializer
{
    public class ListGameSerializer : SerializerBase<ICollection<Game>>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ICollection<Game> games)
        {
            context.Writer.WriteStartArray();

            foreach (var game in games)
            {
                BsonSerializer.Serialize(context.Writer, game.Id);
            }

            context.Writer.WriteEndArray();
        }
    }
}