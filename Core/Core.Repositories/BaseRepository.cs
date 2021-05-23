using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        protected readonly DbSet<T> Entities;

        protected BaseRepository(DbContext context)
        {
            this._context = context;
            this.Entities = context.Set<T>();
        }

        public long GetCount()
        {
            var count = this.Entities.LongCount();
            return count;
        }

        public long GetCount(Expression<Func<T, bool>> expression)
        {
            var count = this.Entities.LongCount(expression);
            return count;
        }

        public T Get(int id)
        {
            var entity = this.Entities.Find(id);
            this._context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            var result = this.Entities.AsQueryable();
            return result;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            var result = this.Entities.Where(expression).AsQueryable();
            return result;
        }

        public T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            this.Entities.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            this.Entities.Remove(entity);
        }

        public void Delete(int id)
        {
            this.Entities.Remove(this.Get(id));
        }
    }
}