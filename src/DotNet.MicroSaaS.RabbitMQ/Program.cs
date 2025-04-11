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
using System.Threading.Tasks;
 
namespace DotNet.MicroSaaS.RabbitMQ
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LoggingConfiguration.Configure();

            try
            {
                await Bootstrap.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Erro fatal ao executar o serviço.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}