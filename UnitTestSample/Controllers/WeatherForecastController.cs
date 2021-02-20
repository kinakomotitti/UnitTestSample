using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestSample.IServices;
using UnitTestSample.Models;

namespace UnitTestSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHeavyWorkOne _heavyWorkOne;
        private readonly IHeavyWorkTwo _heavyWorkTwo;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IHeavyWorkOne heavyWorkOne,
            IHeavyWorkTwo heavyWorkTwo)
        {
            _logger = logger;
            _heavyWorkOne = heavyWorkOne;
            _heavyWorkTwo = heavyWorkTwo;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            this._heavyWorkOne.StartStep1();
            this._heavyWorkOne.StartStep2();
            var date = this._heavyWorkOne.StartFinalStep() ;
            this._heavyWorkTwo.StartStep1();
            this._heavyWorkTwo.StartStep2();
            var summary = this._heavyWorkTwo.StartFinalStep();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = summary
            })
            .ToArray();
        }
    }
}
