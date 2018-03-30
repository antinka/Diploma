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
        private readonly GameStoreContext _db;

        public PlatformTypeRepository(GameStoreContext context)
        {
            _db = context;
        }
        public void Create(PlatformType platformType)
        {
            _db.PlatformTypes.Add(platformType);
        }

        public void Delete(Guid id)
        {
            PlatformType item = _db.PlatformTypes.Find(id);
            if (item != null)
                _db.PlatformTypes.Remove(item);
        }

        public PlatformType Get(Guid id)
        {
            return _db.PlatformTypes.Find(id);
        }

        public IEnumerable<PlatformType> GetAll()
        {
            return _db.PlatformTypes.Where(d => d.IsDelete == false);
        }

        public void Update(PlatformType platformType)
        {
            _db.Entry(platformType).State = EntityState.Modified;
        }
    }
}
