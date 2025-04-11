using System.Threading.Tasks;

namespace DotNet.MicroSaaS.RabbitMQ.Core.Interfaces
{
    public interface IRabbitPublisher
    {
        Task PublishAsync<T>(string exchange, string routingKey, T message);
    }
}