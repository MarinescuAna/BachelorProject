using NLog;
namespace TeamWork.ApplicationLogger
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

