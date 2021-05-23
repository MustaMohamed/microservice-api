using Core.Entities;
using Core.Repositories;
using Core.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Employee.Repositories
{
    public class EmployeeRepository : BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
        }
    }
}