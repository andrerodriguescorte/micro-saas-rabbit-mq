using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.MicroSaaS.RabbitMQ
{
    public static class LoggingConfiguration
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(
                    path: "logs/app.log",
                    rollingInterval: RollingInterval.Infinite,
                    fileSizeLimitBytes: 5_000_000,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 10,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                )
                .CreateLogger();
        }
    }
}
