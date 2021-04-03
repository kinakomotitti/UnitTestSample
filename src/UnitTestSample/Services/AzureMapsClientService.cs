using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using UnitTestSample.Appsettings;
using UnitTestSample.Interfaces;
using UnitTestSample.Models;
using UnitTestSample.Models.AzureMaps;

namespace UnitTestSample.Services
{
    public class AzureMapsClientService : IAzureMapsClientService
    {
        private readonly IOptions<AzureMaps> _options;
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<AzureMaps> _logger;
        public AzureMapsClientService(ILogger<AzureMaps> logger, IOptions<AzureMaps> options, IHttpClientFactory  factory)
        {
            this._options = options;
            this._factory = factory;
            this._logger = logger;
        }

        public (bool requestStatus, CurrentConditionsModel response) WeatherGetCurrentConditions(string query)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append("https://atlas.microsoft.com/weather/currentConditions/json");
            urlBuilder.Append("?api-version=1.0");
            urlBuilder.Append($"&query={query}");
            urlBuilder.Append($"&subscription-key={this._options.Value.PrimaryKey}");
            var result = this.GetRequestCore<CurrentConditionsModel>(urlBuilder.ToString());

            switch (result.statusCode)
            {
                case HttpStatusCode.OK:
                    return (true, result.response);
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    break;
                case null:
                    //Client-side error.
                    break;
                default:
                    break;
            }
            return (false, new CurrentConditionsModel());
        }

        private (HttpStatusCode? statusCode, T response) GetRequestCore<T>(string requestUrl) where T : BaseModel
        {
            try
            {
                var response = this._factory.CreateClient().GetAsync(requestUrl).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (response.StatusCode, null);
                }

                var responseObject = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return (response.StatusCode, responseObject);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                this._logger.LogDebug(ex.ToString());
                throw ex;
            }
        }
    }
}
