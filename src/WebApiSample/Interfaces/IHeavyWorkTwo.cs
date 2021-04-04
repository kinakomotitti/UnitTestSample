using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSample.Interfaces
{
    public interface IHeavyWorkTwo
    {
        public void StartStep1();
        public void StartStep2();
        public string StartFinalStep();
    }
}
