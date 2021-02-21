using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestSample.Interfaces
{
    public interface IHeavyWorkOne
    {
        public void StartStep1();
        public void StartStep2();
        public DateTime StartFinalStep();
    }
}
