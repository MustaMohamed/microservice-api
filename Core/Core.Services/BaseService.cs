using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Repositories.IRepository;
using Core.Services.IService;
using Ideal.Core;

namespace Core.Services
{
    public class BaseService<T> : IEntityService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public long GetCount()
        {
            return this._repository.GetCount();
        }

        public long GetCount(Expression<Func<T, bool>> expression)
        {
            return this._repository.GetCount(expression);
        }

        public T Get(int id)
        {
            return this._repository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this._repository.GetAll();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return this._repository.GetAll(expression);
        }

        public T Add(T entity)
        {
            var res = this._repository.Add(entity);
            this._unitOfWork.Complete();
            return res;
        }

        public T Update(T entity)
        {
            var res = this._repository.Update(entity);
            this._unitOfWork.Complete();
            return res;
        }

        public bool Delete(T entity)
        {
            try
            {
                this._repository.Delete(entity);
                this._unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                this._repository.Delete(id);
                this._unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}