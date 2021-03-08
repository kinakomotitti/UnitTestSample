using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestSampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging=>
                {
                    logging.ClearProviders();
                    logging.AddLog4Net();
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IMessageWriter, MessageWriter>();
                })
                .Build();
            var svc = ActivatorUtilities.CreateInstance<MessageWriter>(host.Services);
            svc.Write();
        }
    }

    public interface IMessageWriter
    {
        void Write();
    }

    public class MessageWriter : IMessageWriter
    {
        private readonly ILogger<MessageWriter> logger;
        private readonly IConfiguration appsettings;

        public MessageWriter(ILogger<MessageWriter> logger, IConfiguration appsettings)
        {
            this.logger = logger;
            this.appsettings = appsettings;
        }

        public void Write() =>
            this.logger.LogInformation($"{appsettings.GetValue<string>("LogMessage")}");
    }
}
