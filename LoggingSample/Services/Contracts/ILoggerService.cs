using System;

namespace StaticSphere.LoggingSample.Services.Contracts
{
    public interface ILoggerService
    {
        void LogDebug(string message, params object[] args);
        void LogError(Exception exception, string message, params object[] args);
    }
}