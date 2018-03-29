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
    public class PlatformTypeRepository : IRepository<PlatformType>
    {
        private GameStoreContext db;

        public PlatformTypeRepository(GameStoreContext context)
        {
            db = context;
        }
        public void Create(PlatformType item)
        {
            db.PlatformTypes.Add(item);
        }

        public void Delete(Guid id)
        {
            PlatformType item = db.PlatformTypes.Find(id);
            if (item != null)
                db.PlatformTypes.Remove(item);
        }

        public PlatformType Get(Guid id)
        {
            return db.PlatformTypes.Find(id);
        }

        public IEnumerable<PlatformType> GetAll()
        {
            return db.PlatformTypes;
        }

        public void Update(PlatformType item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
