using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSample.Controllers.Base;
using WebApiSample.Interfaces;
using WebApiSample.Models;
using WebApiSample.Services;

namespace WebApiSample.Controllers
{
    public class WeatherForecastController : MyBaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHeavyWorkOne _heavyWorkOne;
        private readonly IHeavyWorkTwo _heavyWorkTwo;
        private readonly IWeatherForecastService _weatherForecastService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IHeavyWorkOne heavyWorkOne,
            IHeavyWorkTwo heavyWorkTwo,
            IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _heavyWorkOne = heavyWorkOne;
            _heavyWorkTwo = heavyWorkTwo;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            //this._heavyWorkOne.StartStep1();
            //this._heavyWorkOne.StartStep2();
            //var date = this._heavyWorkOne.StartFinalStep() ;
            //this._heavyWorkTwo.StartStep1();
            //this._heavyWorkTwo.StartStep2();
            //var summary = this._heavyWorkTwo.StartFinalStep();

            return this._weatherForecastService.GetCurrentConditionsOfSelectedLocation("47.641268", "-122.125679");
        }
    }
}
