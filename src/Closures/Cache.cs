using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Closures
{
    public class Cache
    {
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();
        public string GetOrSet(string key, Func<string> func)
        {
            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }
            var value = func();
            _cache.Add(key, value);
            return value;
        }

        public string GetOrSet(string key, Func<string, string> func)
        {
            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }
            var value = func(key);
            _cache.Add(key, value);
            return value;
        }

        public string GetByClosure(string key)
        {
            return GetOrSet(key, () => key);
        }

        public string GetByWithoutClosure(string key)
        {
            return GetOrSet(key, (k) => k);
        }
    }
   
}
