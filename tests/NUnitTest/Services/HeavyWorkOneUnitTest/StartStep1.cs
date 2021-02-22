using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics;
using UnitTestSample.Interfaces;
using UnitTestSample.Services;

namespace NUnitTest.Services.HeavyWorkOneUnitTest
{
    [TestFixture]
    public class StartStep1
    {
        private ILogger<HeavyWorkOne> _logger; 
        private IDatetimeService _datetimeService;

        [SetUp]
        public void SetUp()
        {
            var dateMock= new Mock<IDatetimeService>();
            dateMock.Setup(method => method.GetDateTime()).Returns(new DateTime(1989, 2, 10));
            this._datetimeService = dateMock.Object;

            this._logger= new Mock<ILogger<HeavyWorkOne>>().Object;
        }

        [Test]
        public void CallMethod_ReturnCurrentDatetime()
        {
            //setup
            var targetService = new HeavyWorkOne(this._logger, this._datetimeService);

            //action
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            targetService.StartStep1();
            stopWatch.Stop();
            var actual = Math.Round(stopWatch.Elapsed.TotalSeconds);

            //verify
            var expected = 10;//10 sec.
            Assert.AreEqual(expected, actual);
        }
    }
}
