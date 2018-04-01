using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using System;

namespace GameStore.DAL.EF
{
    public class UnitOfWorkGeneric: IUnitOfWorkGeneric,IDisposable
    {
        private GameStoreContext _db;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<Game> _gameRepository;
        private GenericRepository<Genre> _genreRepository;
        private GenericRepository<PlatformType> _platformTypeRepository;


        public UnitOfWorkGeneric(string connectionString)
        {
            _db = new GameStoreContext(connectionString);
        }

        public IGenericRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new GenericRepository<Comment>(_db);
                return _commentRepository;
            }
        }

        public IGenericRepository<Game> Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GenericRepository<Game>(_db);
                return _gameRepository;
            }
        }

        public IGenericRepository<Genre> Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenericRepository<Genre>(_db);
                return _genreRepository;
            }
        }

        public IGenericRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (_platformTypeRepository == null)
                    _platformTypeRepository = new GenericRepository<PlatformType>(_db);
                return _platformTypeRepository;
            }
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        //private readonly GameStoreContext _db;

        //private readonly Lazy<GenericRepository<Game>> _lazyGameRepository;
        //private readonly Lazy<GenericRepository<Genre>> _lazyGenreRepository;
        //private readonly Lazy<GenericRepository<Comment>> _lazyCommentRepository;
        //private readonly Lazy<GenericRepository<PlatformType>> _lazyPlatformTypeRepository;


        //public UnitOfWorkGeneric(string connectionString)
        //{
        //    _db = new GameStoreContext(connectionString);

        //    _lazyGameRepository = new Lazy<GenericRepository<Game>>(() => new GenericRepository<Game>(_db));
        //    _lazyGenreRepository = new Lazy<GenericRepository<Genre>>(() => new GenericRepository<Genre>(_db));
        //    _lazyCommentRepository = new Lazy<GenericRepository<Comment>>(() => new GenericRepository<Comment>(_db));
        //    _lazyPlatformTypeRepository = new Lazy<GenericRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_db));
        //}

        //public IGenericRepository<Game> Games=> _lazyGameRepository.Value;

        //public IGenericRepository<Genre> Genres => _lazyGenreRepository.Value;

        //public IGenericRepository<Comment> Comments => _lazyCommentRepository.Value;

        //public IGenericRepository<PlatformType> PlatformTypes => _lazyPlatformTypeRepository.Value;

        //public void Save() => _db.SaveChanges();
    }
}
