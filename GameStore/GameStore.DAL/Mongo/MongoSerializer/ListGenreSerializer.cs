using GameStore.DAL.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;

namespace GameStore.DAL.Mongo.MongoSerializer
{
    public class ListGenreSerializer : SerializerBase<ICollection<Genre>>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ICollection<Genre> genres)
        {
            context.Writer.WriteStartArray();

            foreach (var genre in genres)
            {
                BsonSerializer.Serialize(context.Writer, genre.Id);
            }

            context.Writer.WriteEndArray();
        }
    }
}
