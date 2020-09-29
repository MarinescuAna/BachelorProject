using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.AplicationLogin.Logger
{
    public class LoggerService:ILoggerService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogError(string path, string message)
        {
            logger.Error(path + ": " + message);
        }
    }
}

