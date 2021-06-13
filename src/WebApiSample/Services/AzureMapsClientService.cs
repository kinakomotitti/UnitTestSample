using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApiSample.Appsettings;
using WebApiSample.Interfaces;
using WebApiSample.Models;
using WebApiSample.Models.AzureMaps;

namespace WebApiSample.Services
{
    public class AzureMapsClientService : IAzureMapsClientService
    {
        private readonly IOptions<AzureMaps> _options;
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<AzureMaps> _logger;
        public AzureMapsClientService(ILogger<AzureMaps> logger, IOptions<AzureMaps> options, IHttpClientFactory factory)
        {
            this._options = options;
            this._factory = factory;
            this._logger = logger;
        }

        public CurrentConditionsModel WeatherGetCurrentConditions(string query)
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
                    return result.response;
                
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                    this._logger.LogWarning($"{result.statusCode}:{result.response.ToString()}");
                    return new CurrentConditionsModel();
         
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.Unauthorized:
                    this._logger.LogError($"{result.statusCode}:{result.response.ToString()}");
                    return new CurrentConditionsModel();
                
                default:
                    return new CurrentConditionsModel();
            }
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
                return (null, null);
            }
        }
    }
}
