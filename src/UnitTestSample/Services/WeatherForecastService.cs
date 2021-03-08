using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestSample.Interfaces;
using UnitTestSample.Models;
using UnitTestSample.Models.AzureMaps;

namespace UnitTestSample.Services
{

    public class WeatherForecastService : IWeatherForecastService
    {

        private readonly IAzureMapsClientService _azureMapsClient;
        
        public WeatherForecastService(IAzureMapsClientService azureMapsClient)
        {
            this._azureMapsClient = azureMapsClient;
        }

        public List<WeatherForecast> GetCurrentConditionsOfSelectedLocation(string latitude, string longitude)
        {

            var result = this._azureMapsClient.WeatherGetCurrentConditions($"{latitude}%2C{longitude}");
            var forecastList = new List<WeatherForecast>();
            if (result.requestStatus)
            {
                foreach (var item in result.response.results)
                {
                    forecastList.Add(new WeatherForecast()
                    {
                        Date = item.dateTime,
                        Summary = item.phrase,
                        TemperatureC = double.Parse(item.temperature.value)
                    }) ;
                }
            }
            return forecastList;
        }
    }
}
