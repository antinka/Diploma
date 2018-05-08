using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo;
using GameStore.DAL.Mongo.MongoEntities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Repositories
{
    public class OrderDecoratorRepository : GenericRepository<Order>
    {
        private readonly ReadOnlyGenericRepository<MongoOrder> _mongoDataRepository;
        private readonly IMapper _mapper;

        public OrderDecoratorRepository(IDbContext db, MongoContext mongo, IMapper mapper) : base(db, mongo)
        {
           _mongoDataRepository = new ReadOnlyGenericRepository<MongoOrder>(mongo);
            _mapper = mapper;
        }

        public override IEnumerable<Order> GetAll()
        {
            var gameStoreOrders = base.GetAll();
            var northwindOrders = _mongoDataRepository.GetAll();

            var mappedOrders = _mapper.Map<IEnumerable<MongoOrder>, IEnumerable<Order>>(northwindOrders);
            gameStoreOrders = gameStoreOrders.Union(mappedOrders);

            return gameStoreOrders;
        }
    }
}
