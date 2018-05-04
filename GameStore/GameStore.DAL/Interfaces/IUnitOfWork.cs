using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game> Games { get; }

        IGenericRepository<Comment> Comments { get; }

        IGenericRepository<Genre> Genres { get; }

        IGenericRepository<PlatformType> PlatformTypes { get; }

        IGenericRepository<OrderDetail> OrderDetails { get; }

        IGenericRepository<Order> Orders { get; }

        IGenericRepository<Publisher> Publishers { get; }

        IGenericRepository<Shipper> Shippers { get; }

        void Save();
    }
}
