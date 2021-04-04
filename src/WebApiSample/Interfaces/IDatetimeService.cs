using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSample.Interfaces
{
    public interface IDatetimeService
    {
        public DateTime GetDateTime();
        public string GetDateTimeString();
    }
}
