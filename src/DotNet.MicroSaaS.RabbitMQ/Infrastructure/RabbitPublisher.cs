using DotNet.MicroSaaS.RabbitMQ.Core.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNet.MicroSaaS.RabbitMQ.Infrastructure
{
    public class RabbitPublisher : IRabbitPublisher
    {
        private readonly RabbitConnectionManager _manager;
        private readonly ILogger<RabbitPublisher> _logger;

        public RabbitPublisher(RabbitConnectionManager manager, ILogger<RabbitPublisher> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Task PublishAsync<T>(string exchange, string routingKey, T message)
        {
            using var channel = _manager.CreateChannel();

            // Garante que a fila existe antes de publicar
            channel.QueueDeclare(
                queue: routingKey,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange, routingKey, null, body);

            _logger.LogInformation("Mensagem publicada no exchange '{Exchange}' com routing key '{RoutingKey}'", exchange, routingKey);

            return Task.CompletedTask;
        }
    }
}