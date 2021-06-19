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
using TeamWork.DataAccess.Domain.AccountDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Factory
{
    public class TokenGeneratorHelper
    {
        private readonly IConfiguration _configuration;
        public TokenGeneratorHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JWToken GenerateTokenAndSaveTokensInUser(ref User user)
        {
            var token = new JWToken();

            user.AccessToken = token.AccessToken = GenerateToken(
                user.FirstName + Constants.BlankSpace + user.LastName,
                user.UserEmailId, user.UserRole.ToString(), true);
            user.AccessTokenExpiration = token.AccessTokenExpiration = DateTime.Now.AddHours(Number.Number_2);

            user.RefreshTokenExpiration = token.RefershTokenExpiration = DateTime.Now.AddMonths(Number.Number_2);
            user.RefreshToken = token.RefershToken = GenerateToken(
                user.FirstName + Constants.BlankSpace + user.LastName,
                user.UserEmailId,
                user.UserRole.ToString(),
                false
                );

            return token;
        }
        private string GenerateToken(string userName, string userEmail, string role, bool IsAccessToken)
        {
            var key = Encoding.ASCII.GetBytes(_configuration[Constants.SecretKey]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = IsAccessToken ?
                    DateTime.UtcNow.AddHours(Number.Number_2):
                    DateTime.UtcNow.AddMonths(Number.Number_2),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}
