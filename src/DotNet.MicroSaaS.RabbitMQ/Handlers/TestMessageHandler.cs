using DotNet.MicroSaaS.RabbitMQ.Core.Interfaces;
using DotNet.MicroSaaS.RabbitMQ.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNet.MicroSaaS.RabbitMQ.Handlers
{
    public class TestMessageHandler : IRabbitConsumerHandler<TestMessage>
    {
        private readonly ILogger<TestMessageHandler> _logger;

        public TestMessageHandler(ILogger<TestMessageHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(TestMessage message)
        {
            _logger.LogInformation("Mensagem recebida: {Texto}", message.Texto);
            return Task.CompletedTask;
        }
    }
}