using EntityFrameworkSample.Interfaces;
using EntityFrameworkSample.Models;
using EntityFrameworkSample.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EntityFrameworkSample
{
    class Program
    {
        private readonly IManageUserService _targetService;

        public Program(IManageUserService targetService)
        {
            _targetService = targetService;
        }

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<sampleContext>(options =>
                    {
                        options.UseNpgsql("Host=localhost;Database=sample;Username=postgres;Password=dotnet");
                    });
                    services.AddTransient<Program>();
                    services.AddTransient<IManageUserService, ManageUserService>();
                })
                .Build().Services
                .GetRequiredService<Program>()
                .Run(args);
        }

        public void Run(string[] args)
        {
            Console.WriteLine("Hello");
            Console.WriteLine(this._targetService.GetUserId());
        }
    }
}
