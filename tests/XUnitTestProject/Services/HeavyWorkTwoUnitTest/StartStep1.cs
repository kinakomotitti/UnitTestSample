using Moq;
using System;
using UnitTestSample;
using UnitTestSample.Interfaces;
using UnitTestSample.Services;
using Xunit;

namespace XUnitTestProject.Services.HeavyWorkTwoUnitTest
{

    public class StartStep1
    {
        public IDatetimeService _datetimeService { get; set; }
        public BloggingContext _dbContext { get; set; }

        public StartStep1()
        {
            var datetimeService = new Mock<IDatetimeService>();
            datetimeService.Setup(method => method.GetDateTime()).Returns(new DateTime(2020,01,01));
            this._datetimeService = datetimeService.Object;

            var dbContext = new Mock<BloggingContext>();
            //dbContext.Setup(method => method.Add(new Blog()));
            //dbContext.Setup(method => method.SaveChanges());
            this._dbContext = dbContext.Object;
        }

        [Fact]
        public void CallMethod_InsertValueToDB()
        {
            var target = new HeavyWorkTwo(this._dbContext, this._datetimeService);
            target.StartStep1();
        }
    }
}
