using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<TEntiy> : IGenericRepository<TEntiy> where TEntiy : class, IBaseEntity
    {
        private readonly IDbContext _db;
        private readonly IDbSet<TEntiy> _dbSet;

        public GenericRepository(IDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntiy>();
        }

        public virtual void Create(TEntiy item)
        {
            if (item != null)
                _dbSet.Add(item);
        }

        public virtual void Delete(Guid id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                item.IsDelete = true;
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
            _db.Set<TEntiy>().AddOrUpdate(item);
        }

        public virtual IEnumerable<TEntiy> Get(Func<TEntiy, bool> predicate)
        {
			return _dbSet.Where(predicate).Where(x => x.IsDelete == false).AsEnumerable();
        }

        public int Count()
        {
            return _dbSet.Count(x => x.IsDelete == false);
        }
    }
}
