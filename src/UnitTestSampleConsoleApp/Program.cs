using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UnitTestSampleConsoleApp
{
    class Program
    {
        private readonly string[] args;

        public Program(string[] args)
        {
            this.args = args;
        }

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<Program>();
                    services.AddSingleton<string[]>(args);
                })
                .Build().Services
                .GetRequiredService<Program>()
                .Run();
        }

        public void Run()
        {
            //anything....

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
