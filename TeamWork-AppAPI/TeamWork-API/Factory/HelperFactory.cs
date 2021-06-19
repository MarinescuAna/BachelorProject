using Microsoft.Extensions.Configuration;
using TeamWork.ApplicationLogger;

namespace TeamWork_API.Factory
{
    public class HelperFactory:IHelperFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _loggerService;
        public HelperFactory(IConfiguration configuration,ILoggerService loggerService)
        {
            _loggerService = loggerService;
            _configuration = configuration;
        }
        public ImageHelper CreateImageHelper()
        {
            return new ImageHelper();
        }
        public SecurityHelper CreateSecurityHelper()
        {
            return new SecurityHelper(_configuration,_loggerService);
        }
        public TokenGeneratorHelper CreateTokenGeneratorHelper()
        {
            return new TokenGeneratorHelper(_configuration);
        }
    }
}
