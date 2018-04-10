using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game> Games { get; }

        IGenericRepository<Comment> Comments { get; }

        IGenericRepository<Genre> Genres { get; }

        IGenericRepository<PlatformType> PlatformTypes { get; }

        void Save();
    }
}
