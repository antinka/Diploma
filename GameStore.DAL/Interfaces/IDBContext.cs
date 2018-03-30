using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
