using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork_API.Utils;

namespace TeamWork_API.Helper
{
    public class JwTokenGeneratorHelper
    {
        private readonly IConfiguration configuration;
        public JwTokenGeneratorHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateAccessToken(string userId, string userEmail, string role)
        {
            var key = Encoding.ASCII.GetBytes(configuration[Constants.SecretKey]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email,userEmail)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public string GenerateRefreshToken(string userId, string userEmail, string date, string role)
        {
            var key = Encoding.ASCII.GetBytes(configuration[Constants.SecretKey]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Expiration, date),
                    new Claim(ClaimTypes.Email,userEmail)
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
