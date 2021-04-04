using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSample.Models.AzureMaps;

namespace WebApiSample.Interfaces
{
    public interface IAzureMapsClientService
    {
        public (bool requestStatus, CurrentConditionsModel response) WeatherGetCurrentConditions(string query);
    }
}
