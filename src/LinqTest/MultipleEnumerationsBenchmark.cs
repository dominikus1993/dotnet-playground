using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace LinqTest
{
    [MemoryDiagnoser]
    public class MultipleEnumerationsBenchmark
    {
        private List<int> _list;
        private IEnumerable<int> _enumerable;

        public MultipleEnumerationsBenchmark()
        {
            _enumerable = Enumerable.Range(0, 100);
            _list = Enumerable.Range(0, 100).ToList();
        }
        
        [Benchmark]
        public void MultipleEnumerations()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (var item in _enumerable)
                {

                }
            }
        }

        [Benchmark]
        public void List()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (var item in _list)
                {

                }
            }
        }
    }
}
