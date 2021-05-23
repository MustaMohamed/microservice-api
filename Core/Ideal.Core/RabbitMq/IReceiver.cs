using System;

namespace Ideal.Core.RabbitMq
{
    public interface IReceiver<T>
    {
        public void WhenReceived(Action<T> callback);
        public void WhenReceivedAsync(Action<T> callback);
    }

    public interface IReceiver
    {
        public void WhenReceived(Action<object> callback);
        public void WhenReceivedAsync(Action<object> callback);
    }
}