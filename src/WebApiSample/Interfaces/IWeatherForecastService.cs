using System.Collections.Generic;
using WebApiSample.Models;
using WebApiSample.Models.AzureMaps;

namespace WebApiSample.Interfaces
{
    public interface IWeatherForecastService
    {
        public List<WeatherForecast> GetCurrentConditionsOfSelectedLocation(string latitude, string longitude);
    }
}
