using GameStore.DAL.Entities;
using System.Data.Entity;

namespace GameStore.DAL.Interfaces
{
    public interface IDbContext
    {
         DbSet<Comment> Comments { get; set; }

         DbSet<Game> Games { get; set; }

         DbSet<Genre> Genres { get; set; }

         DbSet<PlatformType> PlatformTypes { get; set; }
    }
}
