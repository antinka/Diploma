using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Repositories
{
    public class GenericDecoratorRepository<TEntity> : GenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IGenericRepository<TEntity> _mongoDataRepository;

        public GenericDecoratorRepository(IDbContext sql, MongoContext mongo):base(sql, mongo)
        {
            _mongoDataRepository = new ReadOnlyGenericRepository<TEntity>(mongo);
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return base.GetAll().Union(_mongoDataRepository.GetAll());
        }

        public override TEntity GetById(Guid id)
        {
            return base.GetById(id) ?? (_mongoDataRepository.GetById(id));
        }

        public override IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return base.Get(predicate).Union(_mongoDataRepository.Get(predicate));
        }
    }
}
