using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using System;

namespace GameStore.DAL.EF
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GameStoreContext _db;
        private  CommentRepository _commentRepository;
        private  GameRepository _gameRepository;
        private  GenreRepository _genreRepository;
        private  PlatformTypeRepository _platformTypeRepository;


        public UnitOfWork(string connectionString)
        {
            _db = new GameStoreContext(connectionString);
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_db);
                return _commentRepository;
            }
        }

        public IRepository<Game> Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GameRepository(_db);
                return _gameRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_db);
                return _genreRepository;
            }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (_platformTypeRepository == null)
                    _platformTypeRepository = new PlatformTypeRepository(_db);
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

    }
}
