using System;

namespace TeamWork.AplicationLogin.Logger
{
    public interface ILoggerService
    {
        void LogError(string path, string message);
    }
}
