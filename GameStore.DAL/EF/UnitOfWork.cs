using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using System;

namespace GameStore.DAL.EF
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly GameStoreContext _db;

        private readonly Lazy<GenericRepository<Game>> _lazyGameRepository;
        private readonly Lazy<GenericRepository<Genre>> _lazyGenreRepository;
        private readonly Lazy<GenericRepository<Comment>> _lazyCommentRepository;
        private readonly Lazy<GenericRepository<PlatformType>> _lazyPlatformTypeRepository;


        public UnitOfWork(string connectionString)
        {
            _db = new GameStoreContext(connectionString);

            _lazyGameRepository = new Lazy<GenericRepository<Game>>(() => new GenericRepository<Game>(_db));
            _lazyGenreRepository = new Lazy<GenericRepository<Genre>>(() => new GenericRepository<Genre>(_db));
            _lazyCommentRepository = new Lazy<GenericRepository<Comment>>(() => new GenericRepository<Comment>(_db));
            _lazyPlatformTypeRepository = new Lazy<GenericRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_db));
        }

        public IGenericRepository<Game> Games => _lazyGameRepository.Value;

        public IGenericRepository<Genre> Genres => _lazyGenreRepository.Value;

        public IGenericRepository<Comment> Comments => _lazyCommentRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypes => _lazyPlatformTypeRepository.Value;

        public void Save() => _db.SaveChanges();
    }
}
