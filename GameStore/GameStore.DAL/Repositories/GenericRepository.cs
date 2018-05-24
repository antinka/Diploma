using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IDbContext _db;
        private readonly IDbSet<TEntity> _dbSet;

        public GenericRepository(IDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public virtual void Create(TEntity item)
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
            _db.Set<TEntity>().AddOrUpdate(item);
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
			return _dbSet.Where(predicate).Where(x => x.IsDelete == false).ToList();
        }

        public int Count()
        {
            return _dbSet.Count(x => x.IsDelete == false);
        }
    }
}
