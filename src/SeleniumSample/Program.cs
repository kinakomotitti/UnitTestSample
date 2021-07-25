using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumSample.Interfaces;
using SeleniumSample.Services;
using System;
using System.Linq;

namespace SeleniumSample
{
    class Program
    {
        private readonly IYahooTransitService _transitService;

        public Program(IYahooTransitService transitService)
        {
            _transitService = transitService;
        }

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<Program>();
                    services.AddTransient<IYahooTransitService, YahooTransitService>();
                })
                .Build().Services
                .GetRequiredService<Program>()
                .Run(args);
        }

        public void Run(string[] args)
        {
            Console.WriteLine(this._transitService.GetFareOfRoute1("大阪","東京"));
        }
    }
}
