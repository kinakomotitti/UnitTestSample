using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestSample.IServices;

namespace UnitTestSample.Services
{
    public class DatetimeService : IDatetimeService
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public string GetDateTimeString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
