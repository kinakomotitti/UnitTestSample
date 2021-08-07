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
        private readonly IAngularMaterialDatePicker _angularMaterialDatePicker;

        public Program(IYahooTransitService transitService,IAngularMaterialDatePicker angularMaterialDatePicker)
        {
            _transitService = transitService;
            _angularMaterialDatePicker = angularMaterialDatePicker;
        }

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<Program>();
                    services.AddTransient<IYahooTransitService, YahooTransitService>();
                    services.AddTransient<IAngularMaterialDatePicker,AngularMaterialDatePicker>();
                })
                .Build().Services
                .GetRequiredService<Program>()
                .Run(args);
        }

        public void Run(string[] args)
        {
            var time = new DateTime(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            this._angularMaterialDatePicker.SetDatePicker(time);
            //Console.WriteLine(this._transitService.GetFareOfRoute1("大阪","東京"));
        }
    }
}
