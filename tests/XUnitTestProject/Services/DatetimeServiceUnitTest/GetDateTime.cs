using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestSample.Services;
using Xunit;

namespace XUnitTestProject.Services.DatetimeServiceUnitTest
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

    public abstract class BaseTest
    {
        private void PrivateMethod() { }
        protected void ProtectedMethod() { }
        public static void PublicMethod() { }
    }

    public class Test : BaseTest
    {
        //public override void PublicMethod() { }
        //private override void PrivateMethod() { }


        protected void ProtectedMethod() { 
            
        }
    }
}
