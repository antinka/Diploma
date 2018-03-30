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
    public class GameRepository : IRepository<Game>
    {
        private readonly GameStoreContext _db;

        public GameRepository(GameStoreContext context)
        {
            _db = context;
        }
        public void Create(Game game)
        {
             _db.Games.Add(game);
        }

        public void Delete(Guid id)
        {
            Game item = _db.Games.Find(id);
            if (item != null)
                _db.Games.Remove(item);
        }

        public Game Get(Guid id)
        {
            return _db.Games.Find(id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _db.Games.Where(d => d.IsDelete == false);
        }

        public void Update(Game game)
        {
            _db.Entry(game).State = EntityState.Modified;
        }
    }
}
