using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.Logging;

namespace Logging
{
    public class LoggingBenchmark
    {
        public readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Warning));
        private ILogger<LoggingBenchmark> _logger;

        public LoggingBenchmark()
        {
            _logger = new Logger<LoggingBenchmark>(_loggerFactory);
        }
        
        [Benchmark]
        public void LogDefault()
        {
            _logger.LogInformation("Add Two Numbers: {A} + {B} = {Result}", 1, 2, 3);
        }

        [Benchmark]
        public void LogWithIf()
        {
            if(_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Add Two Numbers: {A} + {B} = {Result}", 1, 2, 3);
        }

        [Benchmark]
        public void LogDelegate()
        {
            _logger.LogAddDelegate(1, 2, 3);
        }

        [Benchmark]
        public void LogSourceGen()
        {
            _logger.LogAdd(1, 2, 3);
        }
    }
}
