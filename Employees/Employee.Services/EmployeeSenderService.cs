using System;
using System.Threading.Tasks;
using Employee.Services.IService;
using Ideal.Core;
using RabbitMQ.Client;

namespace Employee.Services
{
    public class EmployeeSenderService : IEmployeeSenderService
    {
        private IConnection _connection;

        public EmployeeSenderService()
        {
            CreateConnection();
        }

        public void Send(object obj)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: "employees", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = obj.Serialize();

                channel.BasicPublish(exchange: "", routingKey: "employees", basicProperties: null, body: body);
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost", UserName = "guest", Password = "guest"
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }

        public async Task SendAsync(object obj)
        {
            //
        }
    }
}