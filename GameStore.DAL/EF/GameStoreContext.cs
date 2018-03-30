using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GameStore.DAL.EF
{
    public class GameStoreContext : DbContext, IDbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }

        public GameStoreContext()
        {
        }

        public GameStoreContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new GameStoreDbInitializer());
        }
    }
}

    
