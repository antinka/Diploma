using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.EF
{
    public class GameStoreContext : DbContext,IDbContext
    {

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }

        static GameStoreContext()
        {
            Database.SetInitializer(new GameStoreDbInitializer());
        }

        public GameStoreContext(string connectionString)
            : base(connectionString)
        {
        }
        
        
    }
}
