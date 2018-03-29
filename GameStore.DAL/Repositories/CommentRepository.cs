using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private GameStoreContext db;

        public CommentRepository(GameStoreContext context)
        {
            db = context;
        }
        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Delete(Guid id)
        {
            Comment item = db.Comments.Find(id);
            if (item != null)
                db.Comments.Remove(item);
        }

        public Comment Get(Guid id)
        {
            return db.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
