using System;
using System.Collections.Generic;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;
using MongoDB.Driver;

namespace GameStore.DAL.Mongo
{
    public class MongoContext 
    {
        readonly IMongoDatabase _database;

        public MongoContext()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Northwind");
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(CollectionName.CollectionNames[typeof(TEntity)]);
        }
    }
    public static class CollectionName
    {
        public static IDictionary<Type, string> CollectionNames
        {
            get
            {
                var collectionNames = new Dictionary<Type, string>()
                {
                    {typeof(MongoOrder), "orders"},
                    {typeof(Shipper), "shippers"},
                    { typeof(Log), "logging"}
                };
                return collectionNames;
            }
        }
    }
}
