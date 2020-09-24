using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AplicationLogic.Service.Models.Interface;
using TeamWork_API.Utils;
using AplicationLogic.Repository.UOW;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using DataAccess.Domain.Account.Domain;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwTokenGenerator tokenGenerator;
        public AccountController(IConfiguration configuration,IUserService userService)
        {
            _userService = userService;
            tokenGenerator = new JwTokenGenerator(configuration);
        }

       
        [HttpPost]
        [Route("/api/Account/Login")]
        public async Task<IActionResult> Login(UserLoginModel credentials)
        {
            if (credentials == null)
            {
                return StatusCode(204, Messages.NoContent_204NoContent);
            }

            User user = await _userService.GetUserByEmail(credentials.EmailAddress);

            if (user == null || user.Password!= credentials.Password)
            {
                return StatusCode(404,Messages.InvalidCredentials_4040NotFound );
            }

            JWToken jWToken = new JWToken();

            jWToken.AccessToken = user.AccessToken = tokenGenerator.GenerateAccessToken(user.UserId.ToString(), user.EmailAddress, user.UserRole.ToString());
            user.AccessTokenExpiration = jWToken.AccessTokenExpiration = DateTime.Now.AddHours(2);
            user.RefreshTokenExpiration = jWToken.RefershTokenExpiration = DateTime.Now.AddMonths(2);
            jWToken.RefershToken = user.RefreshToken = tokenGenerator.GenerateRefreshToken(user.UserId.ToString(), user.EmailAddress, DateTime.Now.AddMonths(2).ToString(), user.UserRole.ToString());
            HttpContext.Session.SetString("Token", user.AccessToken);

            int response = await _userService.UpdateUserInformation(user);

            if(response > 0)
            {
                return StatusCode(201,jWToken);
            }

            return StatusCode(400, Messages.SthWentWrong_400BadRequest);

        }

        [HttpPost]
        [Route("/api/Account/Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterModel userCredentials)
        {
            if (userCredentials == null)
            {
                return StatusCode(204,Messages.NoContent_204NoContent);
            }

            if (await _userService.GetUserByEmail(userCredentials.EmailAddress) != null)
            {
                return StatusCode(409, Messages.UserAlreadyExistLogin_409Conflict);
            }

            User user = new User
            {
                EmailAddress = userCredentials.EmailAddress,
                FirstName = userCredentials.FirstName,
                Institution = userCredentials.Institution,
                LastName = userCredentials.LastName,
                Password = userCredentials.Password,
                UserRole = userCredentials.UserRole == "Teacher" ? Role.TEACHER : Role.STUDENT
            };

            JWToken jWToken = new JWToken();

            user.AccessToken = jWToken.AccessToken = tokenGenerator.GenerateAccessToken(user.UserId.ToString(), user.EmailAddress, user.UserRole.ToString());
            user.AccessTokenExpiration = jWToken.AccessTokenExpiration = DateTime.Now.AddHours(2);
            user.RefreshTokenExpiration = jWToken.RefershTokenExpiration = DateTime.Now.AddMonths(2);
            user.RefreshToken = jWToken.RefershToken = tokenGenerator.GenerateRefreshToken(user.UserId.ToString(), user.EmailAddress, jWToken.RefershTokenExpiration.ToString(), user.UserRole.ToString());
            HttpContext.Session.SetString("Token", user.AccessToken);

            int response = await _userService.InsertUser(user);
            
            if ( response > 0)
            {
                return StatusCode(201, jWToken);
            }

            return StatusCode(400, Messages.SthWentWrong_400BadRequest);
        }
    }
}