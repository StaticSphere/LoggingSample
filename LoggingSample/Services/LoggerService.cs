using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using StaticSphere.LoggingSample.Services.Contracts;

namespace StaticSphere.LoggingSample.Services
{
    [ExcludeFromCodeCoverage]
    public class LoggerService : ILoggerService
    {
        public ILogger _logger;

        public LoggerService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ApplicationLogger");
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }
    }
}