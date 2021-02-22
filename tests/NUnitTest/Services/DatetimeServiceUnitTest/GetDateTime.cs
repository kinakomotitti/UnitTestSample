using NUnit.Framework;
using System;
using UnitTestSample.Services;

namespace NUnitTest.Services.DatetimeServiceUnitTest
{
    [TestFixture]
    public class GetDateTime : BaseUnitTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void GetDatetime_ReturnTheCurrentTime()
        {
            var service = new DatetimeService();
            var expected = DateTime.Now;
            var actual = service.GetDateTime();
            Assert.AreEqual(expected.DayOfWeek, actual.DayOfWeek);
        }

    }
}
