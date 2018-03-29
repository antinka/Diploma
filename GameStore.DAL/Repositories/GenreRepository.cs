using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private GameStoreContext db;

        public GenreRepository(GameStoreContext context)
        {
            db = context;
        }
        public void Create(Genre item)
        {
            db.Genres.Add(item);
        }

        public void Delete(Guid id)
        {
            Genre item = db.Genres.Find(id);
            if (item != null)
                db.Genres.Remove(item);
        }

        public Genre Get(Guid id)
        {
            return db.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return db.Genres;
        }

        public void Update(Genre item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
