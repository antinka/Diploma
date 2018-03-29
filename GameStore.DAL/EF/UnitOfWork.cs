using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;

namespace GameStore.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private GameStoreContext db;
        private CommentRepository commentRepository;
        private GameRepository gameRepository;
        private GenreRepository genreRepository;
        private PlatformTypeRepository platformTypeRepository;


        public UnitOfWork(string connectionString)
        {
            db = new GameStoreContext(connectionString);
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new CommentRepository(db);
                return commentRepository;
            }
        }

        public IRepository<Game> Games
        {
            get
            {
                if (gameRepository == null)
                    gameRepository = new GameRepository(db);
                return gameRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (platformTypeRepository == null)
                    platformTypeRepository = new PlatformTypeRepository(db);
                return platformTypeRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
