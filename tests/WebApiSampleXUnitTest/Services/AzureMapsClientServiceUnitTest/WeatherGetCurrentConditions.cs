using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiSample.Appsettings;
using WebApiSample.Models;
using WebApiSample.Models.AzureMaps;
using WebApiSample.Services;
using Xunit;

namespace WebApiSampleXUnitTest.Services.AzureMapsClientServiceUnitTest
{
    public class WeatherGetCurrentConditions : BaseUnitTest
    {
        [Fact]
        public void GetWeatherInfo_StatusCode200_ReturnSuccess()
        {
            #region Arange

            var loggerStub = new Mock<ILogger<AzureMaps>>();
            var logger = loggerStub.Object;

            var iOptionsStub = new AzureMaps();
            var iOptions = Options.Create<AzureMaps>(iOptionsStub);

            //The best way to create a mock HTTPClient instance is by mocking HttpMessageHandler. 
            //Mocking of HttpMessageHandler will also ensure the client calling actual endpoints are faked by intercepting it.
            var httpMessageHandlerStub = this.GetHttpResponseMessageStub<CurrentConditionsModel>(new CurrentConditionsModel()
            {
                results = new Result[] {
                (new Result()
                {
                    dateTime=DateTime.Now,
                    cloudCover="cloudCover",
                })
            }
            }, HttpStatusCode.OK);

            var httpClientStub = new HttpClient(httpMessageHandlerStub.Object);
            var httpClientFactoryStub = new Mock<IHttpClientFactory>();
            var actualHttpRequestUrl = string.Empty;
            httpClientFactoryStub.Setup(m => m.CreateClient(It.IsAny<string>()))
                .Callback<string>(url => actualHttpRequestUrl = url)
                .Returns(httpClientStub);
            var httpClientFactory = httpClientFactoryStub.Object;

            #endregion

            #region Action

            var targetService = new AzureMapsClientService(logger, iOptions, httpClientFactory);
            var actual = targetService.WeatherGetCurrentConditions(string.Empty);

            #endregion

            #region Assert

            Assert.Equal("cloudCover", actual.results.Single().cloudCover);
            Assert.Equal("https://atlas.microsoft.com/weather/currentConditions/json?api-version=1.0&subscription-key=プライマリーキー（Configに設定された値）", actualHttpRequestUrl);

            #endregion
        }

        [Fact]
        public void GetWeatherInfo_ThrowException_ReturnEmptyInstance()
        {
            #region Arange

            var loggerStub = new Mock<ILogger<AzureMaps>>();
            var logger = loggerStub.Object;

            var iOptionsStub = new AzureMaps();
            var iOptions = Options.Create<AzureMaps>(iOptionsStub);

            var httpClientFactoryStub = new Mock<IHttpClientFactory>();
            var actualHttpRequestUrl = string.Empty;
            httpClientFactoryStub.Setup(m => m.CreateClient(It.IsAny<string>()))
                .Callback<string>(url => actualHttpRequestUrl = url)
                .Throws(new Exception("Test Case8"));
            var httpClientFactory = httpClientFactoryStub.Object;

            #endregion

            #region Action

            var targetService = new AzureMapsClientService(logger, iOptions, httpClientFactory);
            var actual = targetService.WeatherGetCurrentConditions(string.Empty);

            #endregion

            #region Assert

            Assert.Null( actual.results);

            #endregion
        }
    }
}
