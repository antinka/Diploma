using System;
using System.Collections.Generic;
using System.Configuration;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;
using MongoDB.Driver;

namespace GameStore.DAL.Mongo
{
    public class MongoContext 
    {
        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["GameStoreMongoContext"].ConnectionString;
            //var client = new MongoClient(connectionString);
            //var dbName = MongoUrl.Create(connectionString).DatabaseName;

            //_database = client.GetDatabase(dbName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(CollectionName.CollectionNames[typeof(TEntity)]);
        }
    }
}
