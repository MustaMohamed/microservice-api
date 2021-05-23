using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Services.IService
{
    public interface IEntityService<T> : IService where T : class
    {
        long GetCount();
        long GetCount(Expression<Func<T, bool>> expression);

        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);
    }
}