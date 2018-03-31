using GameStore.DAL.Entities;
using GameStore.DAL.Repositories;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWorkGeneric
    {
        IGenericRepository<Game> Games { get; }
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Genre> Genres { get; }
        IGenericRepository<PlatformType> PlatformTypes { get; }
        void Save();
    }
}
