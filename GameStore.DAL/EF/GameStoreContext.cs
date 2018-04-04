﻿using GameStore.DAL.Entities;
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
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}

    
