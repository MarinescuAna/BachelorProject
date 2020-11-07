using System;

namespace TeamWork.ApplicationLogger
{
    public interface ILoggerService
    {
        void LogError(string path, string message);
    }
}
