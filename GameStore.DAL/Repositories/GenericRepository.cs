using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<TEntiy> : IGenericRepository<TEntiy> where TEntiy : class, IBaseEntity
    {
        private readonly GameStoreContext _db;
        private DbSet<TEntiy> _dbSet;

        public GenericRepository(GameStoreContext _db)
        {
            this._db = _db;
            this._dbSet = _db.Set<TEntiy>();
        }

        public virtual void Create(TEntiy item)
        {
            if (item != null)
                _dbSet.Add(item);
        }

        public virtual void Delete(Guid id)
        {
            TEntiy item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }

        public virtual TEntiy Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntiy> GetAll()
        {
            return _dbSet;
        }

        public virtual void Update(TEntiy item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
