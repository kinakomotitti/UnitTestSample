using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestSample.Interfaces
{
    public interface IDatetimeService
    {
        public DateTime GetDateTime();
        public string GetDateTimeString();
    }
}
