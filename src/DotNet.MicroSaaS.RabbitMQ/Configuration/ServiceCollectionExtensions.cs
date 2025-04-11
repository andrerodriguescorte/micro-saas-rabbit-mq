using DotNet.MicroSaaS.RabbitMQ.Core.Interfaces;
using DotNet.MicroSaaS.RabbitMQ.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNet.MicroSaaS.RabbitMQ.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services, string uri)
        {
            services.AddSingleton(new RabbitConnectionManager(uri));
            services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
            return services;
        }

        public static IServiceCollection AddRabbitConsumer<TMessage, THandler>(this IServiceCollection services, string queueName)
            where THandler : class, IRabbitConsumerHandler<TMessage>
        {
            services.AddSingleton(provider =>
            {
                var manager = provider.GetRequiredService<RabbitConnectionManager>();
                var handler = provider.GetRequiredService<THandler>();
                var logger = provider.GetRequiredService<ILogger<RabbitConsumer<TMessage>>>();

                return new RabbitConsumer<TMessage>(manager, queueName, handler, logger);
            });

            return services;
        }
    }
}