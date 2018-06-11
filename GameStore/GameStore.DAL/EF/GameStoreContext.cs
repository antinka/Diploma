using System.Data.Entity;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Migrations;

namespace GameStore.DAL.EF
{
    public class GameStoreDBContext : DbContext, IDbContext
    {
        public GameStoreDBContext() : base("DefaultConnection")
        {
        }

        public GameStoreDBContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
