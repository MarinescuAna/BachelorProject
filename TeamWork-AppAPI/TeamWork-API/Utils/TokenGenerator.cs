using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;

namespace TeamWork_API.Utils
{
    public static class TokenGenerator
    {
        public static string GenerateAccessToken(IConfiguration _configuration,string userName, string userEmail, string role)
        {
            var key = Encoding.ASCII.GetBytes(_configuration[Constants.SecretKey]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email,userEmail),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.Name,userName)
                }),
                Expires = DateTime.UtcNow.AddHours(Number.Number_2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public static string GenerateRefreshToken(IConfiguration _configuration,string userName, string userEmail, string date, string role)
        {
            var key = Encoding.ASCII.GetBytes(_configuration[Constants.SecretKey]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Expiration, date),
                    new Claim(ClaimTypes.Email,userEmail),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.Name,userName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}
