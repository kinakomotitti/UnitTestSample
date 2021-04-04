using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.Services;
using Xunit;

namespace WebApiSampleXUnitTest.Services.DatetimeServiceUnitTest
{

    public class GetDateTime
    {
        [Fact]
        public void GetDatetime_ReturnTheCurrentTime()
        {
            var service = new DatetimeService();
            var expected = DateTime.Now;
            var actual = service.GetDateTime();
            Assert.Equal(expected.DayOfWeek, actual.DayOfWeek);
        }
    }
}
