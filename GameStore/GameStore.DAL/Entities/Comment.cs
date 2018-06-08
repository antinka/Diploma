using System;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Mongo.MongoSerializer;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        public virtual Comment ParentComment { get; set; }

        public string Quote { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        [BsonSerializer(typeof(ListGameSerializer))]
        public virtual Game Game { get; set; }
    }
}
