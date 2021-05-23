using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Entities;
using Core.Services.IService;
using Ideal.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Task.Services.IService;

namespace Task.Services
{
    public class EmployeeReceiverService : BackgroundService, IEmployeeReceiverService
    {
        private readonly ICollection<Action<EmployeeEntity>> callbacks = new List<Action<EmployeeEntity>>();
        private IConnection _connection;
        private IModel _channel;
        private ITaskService _taskService;
        private IServiceProvider _services { get; }
        
        public EmployeeReceiverService(IServiceProvider services)
        {
            _services = services;
            InitializeRabbitMqListener();
        }

        public void WhenReceived(Action<EmployeeEntity> callback)
        {
            callbacks.Add(callback);
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost", UserName = "guest", Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "employees", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }


        public void WhenReceivedAsync(Action<EmployeeEntity> callback)
        {
            // 
        }

        protected override System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, ea) =>
            {
                var content = ea.Body.ToArray();
                var employee = content.Deserialize<EmployeeEntity>();
                this.WhenEmployeeUpdated(employee);
                // _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("employees", false, consumer);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        private void WhenEmployeeUpdated(EmployeeEntity employeeEntity)
        {
            using var scope = _services.CreateScope();
            _taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();
            var tasksRelatedToEmployee = this._taskService.GetAll((task) => task.EmployeeId == employeeEntity.Id).ToList();
            var fullName = $"{employeeEntity.FirstName} {employeeEntity.LastName}";
            foreach (var task in tasksRelatedToEmployee)
            {
                task.EmployeeFullName = fullName;
                this._taskService.Update(task);
            }
        }
    }
}