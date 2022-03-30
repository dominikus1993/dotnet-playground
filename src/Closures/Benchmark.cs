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

    [MemoryDiagnoser]
    public class MethodGroupBenchmark
    {

        [Benchmark]
        public void MethodGroupTest()
        {
            MethodGroup.Sum(1, 2, MethodGroup.Add);
        }

        [Benchmark]
        public void Lambda()
        {
            MethodGroup.Sum(1, 2, (x, y) => MethodGroup.Add(x, y));
        }
    }
}
