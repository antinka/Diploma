using GameStore.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GameStore.DAL.Interfaces
{
    public interface IDbContext
    {
        DbSet<Comment> Comments { get; set; }

        DbSet<Game> Games { get; set; }

        DbSet<Genre> Genres { get; set; }

        DbSet<PlatformType> PlatformTypes { get; set; }

        DbSet<OrderDetail> OrderDetails { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<Publisher> Publishers { get; set; }

        int SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
