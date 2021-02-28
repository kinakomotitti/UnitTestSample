using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestSample.Models.AzureMaps;

namespace UnitTestSample.Interfaces
{
    public interface IAzureMapsClientService
    {
        public (bool requestStatus, CurrentConditionsModel response) WeatherGetCurrentConditions(string query);
    }
}
