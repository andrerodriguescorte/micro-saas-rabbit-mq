using RabbitMQ.Client;
using System;

namespace DotNet.MicroSaaS.RabbitMQ.Infrastructure
{
    public class RabbitConnectionManager : IDisposable
    {
        private readonly IConnection _connection;

        public RabbitConnectionManager(string uri)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(uri),
                DispatchConsumersAsync = true
            };
            _connection = factory.CreateConnection();
        }

        public IModel CreateChannel() => _connection.CreateModel();

        public void Dispose() => _connection?.Dispose();
    }
}