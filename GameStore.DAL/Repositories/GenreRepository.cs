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
        private readonly GameStoreContext _db;

        public GenreRepository(GameStoreContext context)
        {
            _db = context;
        }
        public void Create(Genre genre)
        {
            _db.Genres.Add(genre);
        }

        public void Delete(Guid id)
        {
            Genre item = _db.Genres.Find(id);
            if (item != null)
                _db.Genres.Remove(item);
        }

        public Genre Get(Guid id)
        {
            return _db.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return _db.Genres.Where(d => d.IsDelete == false);
        }

        public void Update(Genre genre)
        {
            _db.Entry(genre).State = EntityState.Modified;
        }
    }
}
