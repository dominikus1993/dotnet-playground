using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Loggers;

using Microsoft.Extensions.Logging;

namespace Closures
{
    [MemoryDiagnoser]
    public class ClosuresBenchmark
    {
        private Cache _cache;

        public ClosuresBenchmark()
        {
            _cache = new Cache();
        }
        
        [Benchmark]
        public void GetWithoutClosure()
        {
            _cache.GetByWithoutClosure("closure");
        }

        [Benchmark]
        public void GetWithClosure()
        {
            _cache.GetByClosure("xddddd");
        }
    }
}
