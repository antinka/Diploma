using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
//todo using
using System.Data.Entity.Migrations;
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
			//todo please return IEnumerable, not IQuarable.
            return _dbSet.Where(x => x.IsDelete == false);
        }

        public virtual void Update(TEntiy item)
        {
           _db.Entry(item).State = EntityState.Modified;
        }

		//todo why you need same methods with different names?
        public virtual IEnumerable<TEntiy> Get(Func<TEntiy, bool> predicate)
        {
			//todo please return IEnumerable, not IQuarable.
			return _dbSet.Where(predicate).Where(x => x.IsDelete == false);
        }

		//todo why you need same methods with different names?
		public virtual IEnumerable<TEntiy> Find(Func<TEntiy, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}
