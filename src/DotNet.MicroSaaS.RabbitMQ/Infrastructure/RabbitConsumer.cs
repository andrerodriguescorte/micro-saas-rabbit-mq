using DotNet.MicroSaaS.RabbitMQ.Core.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DotNet.MicroSaaS.RabbitMQ.Infrastructure
{
    public class RabbitConsumer<T>
    {
        public RabbitConsumer(RabbitConnectionManager manager, string queueName, IRabbitConsumerHandler<T> handler, ILogger<RabbitConsumer<T>> logger)
        {
            var channel = manager.CreateChannel();
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (_, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var message = JsonSerializer.Deserialize<T>(json);

                    logger.LogInformation("Mensagem recebida da fila {Queue}: {Conteudo}", queueName, message);

                    await handler.HandleAsync(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erro ao consumir mensagem da fila {Queue}", queueName);
                    // Possível: channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
        }
    }
}