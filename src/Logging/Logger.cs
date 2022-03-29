using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace Logging
{
    public static partial class SourceGenLogger
    {
        [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Add Two Numbers: {A} + {B} = {Result}")]
        public static partial void LogAdd(this ILogger logger, int a, int b, int result);
    }
    
    public static class LoggerDelegate
    {
        private static readonly Action<ILogger, int, int, int, Exception?> _logAdd = LoggerMessage.Define<int, int, int>(LogLevel.Information, new EventId(0, "Add Two Numbers"), "Add Two Numbers: {A} + {B} = {Result}");
        
        public static void LogAddDelegate(this ILogger logger, int a, int b, int result)
        {
            _logAdd(logger, a, b, result, null);
        }
    }
}
