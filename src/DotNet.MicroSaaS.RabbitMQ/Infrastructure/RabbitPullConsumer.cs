using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;

namespace DotNet.MicroSaaS.RabbitMQ.Infrastructure
{
    public class RabbitPullConsumer
    {
        private readonly RabbitConnectionManager _manager;
        private readonly ILogger<RabbitPullConsumer> _logger;

        public RabbitPullConsumer(RabbitConnectionManager manager, ILogger<RabbitPullConsumer> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public T ObterMensagem<T>(string queueName)
        {
            using var channel = _manager.CreateChannel();

            // Garante que a fila existe
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            var result = channel.BasicGet(queueName, autoAck: true);

            if (result == null)
            {
                _logger.LogInformation("Nenhuma mensagem encontrada na fila: {Queue}", queueName);
                return default;
            }

            var body = result.Body.ToArray();
            var message = JsonSerializer.Deserialize<T>(body);

            _logger.LogInformation("Mensagem lida da fila {Queue}: {Conteudo}", queueName, message);

            return message;
        }
    }
}