using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GameStore.DAL.Repositories
{
    public class ReadOnlyGenericRepository<TEntiy> : IGenericRepository<TEntiy> where TEntiy : class
    {
        private readonly MongoContext _db;
    
        public ReadOnlyGenericRepository(MongoContext db)
        {
            _db = db;
        }

        public IEnumerable<TEntiy> GetAll()
        {
            return _db.GetCollection<TEntiy>().AsQueryable().ToList();
        }

        public TEntiy GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Create(TEntiy item)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntiy item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntiy> Get(Func<TEntiy, bool> predicate)
        {
            return _db.GetCollection<TEntiy>().AsQueryable().ToList().Where(predicate);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntiy> Find(Func<TEntiy, bool> predicate)
        {
            return _db.GetCollection<TEntiy>().AsQueryable().ToList().Where(predicate);
        }
    }
}
