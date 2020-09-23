using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork_API.Utils
{
    public class JWToken
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefershToken { get; set; }
        public DateTime RefershTokenExpiration { get; set; }
    }
}
