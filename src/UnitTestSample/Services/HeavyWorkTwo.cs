using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestSample.Interfaces;

namespace UnitTestSample.Services
{
    public class HeavyWorkTwo : IHeavyWorkTwo
    {
        private BloggingContext _context;
        private readonly IDatetimeService _datetimeService;
        public HeavyWorkTwo(BloggingContext context,IDatetimeService datetimeService)
        {
            _context = context;
            _datetimeService = datetimeService;
        }

        public string StartFinalStep()
        {
            return _context.Blogs.OrderBy(row=>row.BlogId)
                                 .LastOrDefault().Url;
        }

        public void StartStep1()
        {
            _context.Add(new Blog { Url = $"http://blogs.msdn.com/adonet/{this._datetimeService.GetDateTimeString()}" });
            _context.SaveChanges();
        }

        public void StartStep2()
        {
            //throw new NotImplementedException();
        }
    }
}
