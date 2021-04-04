using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSample.Interfaces;

namespace WebApiSample.Services
{
    public class HeavyWorkOne : IHeavyWorkOne
    {
        private readonly ILogger<HeavyWorkOne> _logger;
        private readonly IDatetimeService _datetimeService;

        public HeavyWorkOne() { }

        public HeavyWorkOne(ILogger<HeavyWorkOne> logger,IDatetimeService datetimeService)
        {
            this._logger = logger;
            this._datetimeService = datetimeService;
        }
        public DateTime StartFinalStep()
        {
            return this._datetimeService.GetDateTime();
        }

        public void StartStep1()
        {
            this._logger.LogInformation(this._datetimeService.GetDateTimeString());
            Task.Delay(10000).Wait();
            this._logger.LogInformation(this._datetimeService.GetDateTimeString());
        }

        public void StartStep2()
        {
            throw new NotImplementedException();
        }
    }
}
