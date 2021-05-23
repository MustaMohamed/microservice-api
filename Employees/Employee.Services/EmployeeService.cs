using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories.IRepository;
using Core.Services;
using Core.Services.IService;
using Employee.Services.IService;
using Ideal.Core;
using Ideal.Core.RabbitMq;

namespace Employee.Services
{
    public class EmployeeService : BaseService<EmployeeEntity>, IEmployeeService
    {
        private readonly ISender _sender;

        public EmployeeService(IEmployeeRepository repository, IUnitOfWork unitOfWork, IEmployeeSenderService sender) : base(repository, unitOfWork)
        {
            _sender = sender;
        }

        public void PublishUpdateEmployeeEvent(EmployeeEntity entity)
        {
            _sender.Send(entity);
        }

        public Task PublishUpdateEmployeeEventAsync(EmployeeEntity entity)
        {
            return _sender.SendAsync(entity);
        }
    }
}