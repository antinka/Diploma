using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Data.Entity;

namespace GameStore.DAL.EF
{
    public class GameStoreDBContext : DbContext, IDbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public GameStoreDBContext()
        {
        }

        public GameStoreDBContext(string connectionString)
            : base(connectionString)
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}


    
