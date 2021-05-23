using Ideal.Core.RabbitMq;

namespace Employee.Services.IService
{
    public interface IEmployeeSenderService : Core.Services.IService.IService, ISender
    {
    }
}