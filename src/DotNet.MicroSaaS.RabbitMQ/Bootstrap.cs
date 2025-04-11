using DotNet.MicroSaaS.RabbitMQ.Configuration;
using DotNet.MicroSaaS.RabbitMQ.Core.Interfaces;
using DotNet.MicroSaaS.RabbitMQ.Handlers;
using DotNet.MicroSaaS.RabbitMQ.Infrastructure;
using DotNet.MicroSaaS.RabbitMQ.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.MicroSaaS.RabbitMQ
{
    public static class Bootstrap
    {
        public static async Task RunAsync(string queueName = "fila-teste")
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });

            string rabbitUri = configuration["RabbitMQ:Uri"]
                ?? throw new InvalidOperationException("Configuração 'RabbitMQ:Uri' não encontrada.");

            services.AddRabbit(rabbitUri);
            services.AddSingleton(configuration);
            services.AddSingleton<IRabbitConsumerHandler<TestMessage>, TestMessageHandler>();
            services.AddRabbitConsumer<TestMessage, TestMessageHandler>(queueName);
            services.AddSingleton<RabbitPullConsumer>();

            using var provider = services.BuildServiceProvider();
            var logger = provider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Aplicação iniciada com sucesso.");

            var publisher = provider.GetRequiredService<IRabbitPublisher>();
            await publisher.PublishAsync("", queueName, new TestMessage { Texto = "Olá via NuGet!" });

            var pullConsumer = provider.GetRequiredService<RabbitPullConsumer>();

            logger.LogInformation("Publicando mensagens...");
            await PublicarMensagensEmFilasDinamicasAsync(publisher, 10000, queueName);

            logger.LogInformation("Consumindo mensagens...");
            ConsumirMensagensDeFilasDinamicas(pullConsumer, 1000, queueName);

            logger.LogInformation("Processo finalizado.");

            // Mantém o processo ativo após a execução, se desejar
            logger.LogInformation("Consumer aguardando mensagens. Pressione Ctrl+C para sair...");
            await Task.Delay(-1);
        }

        private static void ConsumirMensagensDeFilasDinamicas(RabbitPullConsumer pullConsumer, int total, string queueName)
        {
            int totalConsumido = 0;
            int totalFilas = ((total - 1) / 10) + 1;

            for (int grupo = 1; grupo <= totalFilas; grupo++)
            {
                string fila = $"{queueName}-{grupo}";
                TestMessage msg;
                int count = 0;

                while ((msg = pullConsumer.ObterMensagem<TestMessage>(fila)) != null)
                {
                    count++;
                    totalConsumido++;
                    Console.WriteLine($"[CONSUME] {fila} #{count} → {msg.Texto}");
                }

                if (count == 0)
                    Console.WriteLine($"[CONSUME] {fila} vazia.");
            }

            Console.WriteLine($"Total geral de mensagens consumidas: {totalConsumido}");
        }

        private static async Task PublicarMensagensEmFilasDinamicasAsync(IRabbitPublisher publisher, int total, string queueName)
        {
            for (int i = 1; i <= total; i++)
            {
                // A cada 10 mensagens, muda de fila
                int grupo = ((i - 1) / 10) + 1;
                string fila = $"{queueName}-{grupo}";

                var msg = new TestMessage { Texto = $"Mensagem #{i} na {fila}" };
                await publisher.PublishAsync("", fila, msg);

                Console.WriteLine($"[PUBLISH] #{i} → {fila}");
            }
        }

        private static async Task PublicarVariasMensagensAsync(IRabbitPublisher publisher, int quantidade, string queueName)
        {
            for (int i = 1; i <= quantidade; i++)
            {
                var msg = new TestMessage { Texto = $"Mensagem #{i}" };
                await publisher.PublishAsync("", queueName, msg);
                Console.WriteLine($"[PUBLISH] Mensagem #{i} publicada.");
            }
        }

        private static void ConsumirTodasAsMensagens(RabbitPullConsumer pullConsumer, string queueName)
        {
            int contador = 0;
            TestMessage msg;

            while ((msg = pullConsumer.ObterMensagem<TestMessage>(queueName)) != null)
            {
                contador++;
                Console.WriteLine($"[CONSUME] #{contador} - {msg.Texto}");
            }

            Console.WriteLine($"Total de mensagens consumidas: {contador}");
        }
    }
}
