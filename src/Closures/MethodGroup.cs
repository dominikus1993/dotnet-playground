using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Closures
{
    internal class MethodGroup
    {
        public static int Add(int x, int y) => x + y;

        public static int Sum(int x, int y, Func<int, int, int> add) => add(x, y);
    }
}
