using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestSample.IServices
{
    public interface IDatetimeService
    {
        public DateTime GetDateTime();
        public string GetDateTimeString();
    }
}
