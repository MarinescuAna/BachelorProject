using Microsoft.Extensions.Configuration;

namespace TeamWork_API.Factory
{
    public class HelperFactory:IHelperFactory
    {
        private readonly IConfiguration _configuration;
        public HelperFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ImageHelper CreateImageHelper()
        {
            return new ImageHelper();
        }
        public SecurityHelper CreateSecurityHelper()
        {
            return new SecurityHelper(_configuration);
        }
        public TokenGeneratorHelper CreateTokenGeneratorHelper()
        {
            return new TokenGeneratorHelper(_configuration);
        }
    }
}
