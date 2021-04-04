using System;

namespace TeamWork.ApplicationLogger
{
    public interface ILoggerService
    {
        void LogError(string message);
        void LogInfo(string message);
    }
}
