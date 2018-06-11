using System;
using System.Collections.Generic;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;

namespace GameStore.DAL.Mongo
{
    public static class CollectionName
    {
        public static IDictionary<Type, string> CollectionNames
        {
            get
            {
                var collectionNames = new Dictionary<Type, string>()
                {
                    { typeof(MongoOrder), "orders" },
                    { typeof(Shipper), "shippers" },
                    { typeof(Log), "logging" }
                };
                return collectionNames;
            }
        }
    }
}
