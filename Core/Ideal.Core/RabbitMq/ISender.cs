using System.Threading.Tasks;

namespace Ideal.Core.RabbitMq
{
    public interface ISender
    {
        public void Send(object obj);
        public Task SendAsync(object obj);
    }
}