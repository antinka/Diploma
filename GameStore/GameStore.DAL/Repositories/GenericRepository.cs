using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Enums;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo;
using MongoDB.Bson;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IDbContext _db;
        private readonly IDbSet<TEntity> _dbSet;
        private readonly MongoContext _mongoDb;

        public GenericRepository(IDbContext db, MongoContext mongoDb)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
            _mongoDb = mongoDb;
        }

        public virtual void Create(TEntity item)
        {
            if (item != null)
            {
                _dbSet.Add(item);
            }

            Log(ActionInRepository.Update, item.GetType().ToString(), item.ToJson(), null);
        }

        public virtual void Delete(Guid id)
        {
            var item = _dbSet.Find(id);

            if (item != null)
            {
                item.IsDelete = true;
            }

            Log(ActionInRepository.Update, item.GetType().ToString(), item.ToJson(), null);
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.Where(x => x.IsDelete == false).ToList();
        }

        public virtual void Update(TEntity item)
        {
            var oldObject = GetById(item.Id);

            _db.Set<TEntity>().AddOrUpdate(item);

            Log(ActionInRepository.Update, item.GetType().ToString(), item.ToJson(), oldObject.ToJson());
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public int Count()
        {
            return _dbSet.Count(x => x.IsDelete == false);
        }

        protected void Log(ActionInRepository action, string type, string newObject, string oldObject)
        {
            var log = new Log()
            {
                DateTime = DateTime.UtcNow,
                Action = action.ToString(),
                EntityType = type,
                NewObject = newObject,
                OldObject = oldObject
            };

            _mongoDb.GetCollection<Log>().InsertOne(log);
        }
    }
}
