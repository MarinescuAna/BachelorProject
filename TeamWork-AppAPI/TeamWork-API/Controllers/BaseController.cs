using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;
using TeamWork_API.Utils;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 
        private readonly IConfiguration _configuration;
        protected BaseController(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        protected string ExtractEmailFromJWT()
        {
            return  _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
        protected string GenerateAccessToken(string userName, string userEmail, string role)
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
        protected string GenerateRefreshToken(string userName, string userEmail, string date, string role)
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
        protected string DecompressImage(string compressedBase64)
        {
            if (string.IsNullOrEmpty(compressedBase64))
            {
                return string.Empty;
            }

            Chilkat.Compression compress = new Chilkat.Compression
            {
                Algorithm = Constants.Deflate
            };

            Chilkat.BinData binDat = new Chilkat.BinData();
            binDat.AppendEncoded(compressedBase64, Constants.Base64);
            compress.DecompressBd(binDat);

            return binDat.GetEncoded(Constants.Base64);
        }
        protected string CompressImage(string strBase64)
        {
            if (string.IsNullOrEmpty(strBase64))
            {
                return string.Empty;
            }

            Chilkat.Compression compress = new Chilkat.Compression
            {
                Algorithm = Constants.Deflate
            };

            Chilkat.BinData binDat = new Chilkat.BinData();
            // Load the base64 data into a BinData object.
            // This decodes the base64. The decoded bytes will be contained in the BinData.
            binDat.AppendEncoded(strBase64, Constants.Base64);

            // Compress the BinData.
            compress.CompressBd(binDat);

            // Get the compressed data in base64 format:
            return binDat.GetEncoded(Constants.Base64);
        }
    }
}