using GameStore.DAL.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;

namespace GameStore.DAL.Mongo.MongoSerializer
{
    public class ListPlatformTypeSerializer : SerializerBase<ICollection<PlatformType>>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ICollection<PlatformType> platformTypes)
        {
            context.Writer.WriteStartArray();

            foreach (var platformType in platformTypes)
            {
                BsonSerializer.Serialize(context.Writer, platformType.Id);
            }

            context.Writer.WriteEndArray();
        }
    }
}