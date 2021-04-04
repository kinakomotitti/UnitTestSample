using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApiSample.Models;
using Xunit;

namespace WebApiSampleXUnitTest
{
    public class BaseUnitTest
    {
        protected Mock<HttpMessageHandler> GetHttpResponseMessageStub<T>(T responseContents, HttpStatusCode statusCode) where T : BaseModel
        {
            var httpMessageHandlerStub = new Mock<HttpMessageHandler>();
            httpMessageHandlerStub.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                                ItExpr.IsAny<HttpRequestMessage>(),
                                                                                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = statusCode,
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(responseContents))
                });

            return httpMessageHandlerStub;
        }
    }
}
