using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
        public long GetCount();
        public long GetCount(Expression<Func<T, bool>> expression);
        public T Get(int id);
        public IQueryable<T> GetAll();
        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        public T Add(T entity);
        public T Update(T entity);
        public void Delete(T entity);
        public void Delete(int id);
    }
}