using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeamWork_API.Utils;
using Microsoft.AspNetCore.Http;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Account.Domain;
using TeamWork.DataAccess.Domain.Models.Domain;

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
            if (credentials == null || String.IsNullOrEmpty(credentials.EmailAddress))
            {
                return StatusCode(Codes.Number_204, Messages.NoContent_204NoContent);
            }

            User user = await _userService.GetUserByEmailAsync(credentials.EmailAddress);

            if (user == null || user.Password!= credentials.Password)
            {
                return StatusCode(Codes.Number_404,Messages.InvalidCredentials_4040NotFound );
            }

            JWToken jWToken = new JWToken
            {
                AccessToken = user.AccessToken = tokenGenerator.GenerateAccessToken(user.UserId.ToString(), user.EmailAddress, user.UserRole.ToString())
            };
            user.AccessTokenExpiration = jWToken.AccessTokenExpiration = DateTime.Now.AddHours(2);
            user.RefreshTokenExpiration = jWToken.RefershTokenExpiration = DateTime.Now.AddMonths(2);
            jWToken.RefershToken = user.RefreshToken = tokenGenerator.GenerateRefreshToken(user.UserId.ToString(), user.EmailAddress, DateTime.Now.AddMonths(2).ToString(), user.UserRole.ToString());
            HttpContext.Session.SetString(Constants.Token, user.AccessToken);
 
            int response = await _userService.UpdateUserInformation(user);

            if(response > 0)
            {
                return StatusCode(Codes.Number_201,jWToken);
            }

            return StatusCode(Codes.Number_400, Messages.SthWentWrong_400BadRequest);

        }

        [HttpPost]
        [Route("/api/Account/Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterModel userCredentials)
        {
            if (userCredentials == null || String.IsNullOrEmpty(userCredentials.EmailAddress))
            {
                return StatusCode(Codes.Number_204, Messages.NoContent_204NoContent);
            }

            if (await _userService.GetUserByEmailAsync(userCredentials.EmailAddress) != null)
            {
                return StatusCode(Codes.Number_409, Messages.UserAlreadyExistLogin_409Conflict);
            }

            User user = new User
            {
                EmailAddress = userCredentials.EmailAddress,
                FirstName = userCredentials.FirstName,
                Institution = userCredentials.Institution,
                LastName = userCredentials.LastName,
                Password = userCredentials.Password,
                UserRole = userCredentials.UserRole == Constants.Token ? Role.TEACHER : Role.STUDENT
            };

            JWToken jWToken = new JWToken();

            user.AccessToken = jWToken.AccessToken = tokenGenerator.GenerateAccessToken(user.UserId.ToString(), user.EmailAddress, user.UserRole.ToString());
            user.AccessTokenExpiration = jWToken.AccessTokenExpiration = DateTime.Now.AddHours(2);
            user.RefreshTokenExpiration = jWToken.RefershTokenExpiration = DateTime.Now.AddMonths(2);
            user.RefreshToken = jWToken.RefershToken = tokenGenerator.GenerateRefreshToken(user.UserId.ToString(), user.EmailAddress, jWToken.RefershTokenExpiration.ToString(), user.UserRole.ToString());
            HttpContext.Session.SetString(Constants.Token, user.AccessToken);

            int response = await _userService.InsertUser(user);
            
            if ( response > 0)
            {
                return StatusCode(Codes.Number_201, jWToken);
            }

            return StatusCode(Codes.Number_400, Messages.SthWentWrong_400BadRequest);
        }
    }
}