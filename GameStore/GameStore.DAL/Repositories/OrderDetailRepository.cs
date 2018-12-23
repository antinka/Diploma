using System;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        private readonly IDbContext _db;

        public OrderDetailRepository(IDbContext db) : base(db)
        {
            _db = db;
        }

        public override void Delete(Guid id)
        {
            var orderDetail = _db.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                _db.OrderDetails.Remove(orderDetail);
            }
        }
    }
}
