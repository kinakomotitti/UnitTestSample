using System.Collections.Generic;
using UnitTestSample.Models;
using UnitTestSample.Models.AzureMaps;

namespace UnitTestSample.Interfaces
{
    public interface IWeatherForecastService
    {
        public List<WeatherForecast> GetCurrentConditionsOfSelectedLocation(string latitude, string longitude);
    }
}
