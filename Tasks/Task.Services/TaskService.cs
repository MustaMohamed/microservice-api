using Core.Entities;
using Core.Repositories.IRepository;
using Core.Services;
using Core.Services.IService;
using Ideal.Core;
using Ideal.Core.RabbitMq;

namespace Task.Services
{
    public class TaskService : BaseService<TaskEntity>, ITaskService
    {
        private readonly IReceiver<EmployeeEntity> _receiver;

        public TaskService(ITaskRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}