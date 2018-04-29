using System;
using System.Collections.Generic;

namespace GameStore.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(Guid id);

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        int Count();
    }
}
