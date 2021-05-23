using Core.Entities;
using Ideal.Core.RabbitMq;

namespace Task.Services.IService
{
    public interface IEmployeeReceiverService : IReceiver<EmployeeEntity>
    {
        
    }
}