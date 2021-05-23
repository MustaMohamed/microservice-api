using System.Threading.Tasks;
using Core.Entities;

namespace Core.Services.IService
{
    public interface IEmployeeService : IEntityService<EmployeeEntity>
    {
        public void PublishUpdateEmployeeEvent(EmployeeEntity entity);
        public Task PublishUpdateEmployeeEventAsync(EmployeeEntity entity);
    }
}