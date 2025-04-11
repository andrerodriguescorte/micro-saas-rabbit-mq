using System.Threading.Tasks;

namespace DotNet.MicroSaaS.RabbitMQ.Core.Interfaces
{
    public interface IRabbitConsumerHandler<T>
    {
        Task HandleAsync(T message);
    }
}