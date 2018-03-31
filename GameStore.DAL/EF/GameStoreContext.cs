using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Data.Entity;

namespace GameStore.DAL.EF
{
    public class GameStoreContext : DbContext, IDbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }

        public static GameStoreContext Create()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GameStoreContext>());
            return new GameStoreContext();
        }

        public GameStoreContext()
        {
        }

        public GameStoreContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Game>()
            //    .HasMany(s => s.Comments)
            //    .WithRequired(e => e.Game);

            //modelBuilder.Entity<Game>()
            //    .HasMany(c => c.Genres)
            //     .WithMany(s => s.Games)
            //     .Map(a => a.ToTable("GamesGenres"));

            //modelBuilder.Entity<Game>()
            //    .HasMany(c => c.PlatformTypes)
            //    .WithMany(s => s.Games)
            //    .Map(a => a.ToTable("GamesPlatformTypes"));
        }
    }
}

    
