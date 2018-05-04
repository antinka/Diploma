using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Enums;
using GameStore.DAL.Mongo;
using log4net;
using MongoDB.Bson;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<TEntiy> : IGenericRepository<TEntiy> where TEntiy : class, IBaseEntity
    {
        private readonly IDbContext _db;
        private readonly IDbSet<TEntiy> _dbSet;
        private readonly MongoContext _mongoDb;
        private readonly ILog _log;

        public GenericRepository(IDbContext db, MongoContext mongoDb, ILog log)
        {
            _db = db;
            _dbSet = db.Set<TEntiy>();
            _mongoDb = mongoDb;
            _log = log;
        }

        public virtual void Create(TEntiy item)
        {
            if (item != null)
                _dbSet.Add(item);

            Log(ActionInRepository.Update, item.GetType().ToString(), item.ToString(), null);
        }

        public virtual void Delete(Guid id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                item.IsDelete = true;

            Log(ActionInRepository.Update, item.GetType().ToString(), item.ToString(), null);
        }

        public virtual TEntiy GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntiy> GetAll()
        {
            return _dbSet.Where(x => x.IsDelete == false).AsEnumerable();
        }

        public virtual void Update(TEntiy item)
        {
            var oldObject = GetById(item.Id);

            _db.Set<TEntiy>().AddOrUpdate(item);

            Log(ActionInRepository.Update, item.GetType().ToString(), item.ToString(), oldObject.ToString());
        }

        public virtual IEnumerable<TEntiy> Get(Func<TEntiy, bool> predicate)
        {
			return _dbSet.Where(predicate).Where(x => x.IsDelete == false).AsEnumerable();
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
