using Microsoft.EntityFrameworkCore;

namespace Ideal.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        public void Complete()
        {
            this._context.SaveChanges();
        }
    }
}