using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly GameStoreContext _db;

        public CommentRepository(GameStoreContext context)
        {
            _db = context;
        }
        public void Create(Comment comment)
        {
            _db.Comments.Add(comment);
        }

        public void Delete(Guid id)
        {
            Comment item = _db.Comments.Find(id);

            if (item != null)
                _db.Comments.Remove(item);
        }

        public Comment Get(Guid id)
        {
            return _db.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _db.Comments.Where(d=>d.IsDelete==false);
        }

        public void Update(Comment comment)
        {
            _db.Entry(comment).State = EntityState.Modified;
        }
    }
}
