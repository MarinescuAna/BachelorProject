using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork_API.Factory
{
    public interface IHelperFactory
    {
        ImageHelper CreateImageHelper();
        SecurityHelper CreateSecurityHelper();
        TokenGeneratorHelper CreateTokenGeneratorHelper();
    }
}
