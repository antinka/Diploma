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
        private GameStoreContext db;

        public GameRepository(GameStoreContext context)
        {
            db = context;
        }
        public void Create(Game item)
        {
             db.Games.Add(item);
        }

        public void Delete(Guid id)
        {
            Game item = db.Games.Find(id);
            if (item != null)
                db.Games.Remove(item);
        }

        public Game Get(Guid id)
        {
            return db.Games.Find(id);
        }

        public IEnumerable<Game> GetAll()
        {
            return db.Games;
        }

        public void Update(Game item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
