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
using TeamWork_API.ErrorHandler;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        public AccountController(IConfiguration configuration, IUserService userService,IImageService imageService, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
            _userService = userService;
            _imageService = imageService;
        }

        [HttpPost]
        [Route("/api/Account/Login")]
        public async Task<IActionResult> Login(UserLoginModel credentials)
        {
            if (credentials == null || String.IsNullOrEmpty(credentials.EmailAddress))
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            User user = await _userService.GetUserByEmailAsync(credentials.EmailAddress);

            if (user == null)
            {
                return StatusCode(Codes.Number_404, NotFound404Error.InvalidEmail);
            }
            if (user.Password != credentials.Password)
            {
                return StatusCode(Codes.Number_404, NotFound404Error.InvalidPassword);
            }

            JWToken jWToken = new JWToken
            {
                AccessToken = user.AccessToken = GenerateAccessToken(user.FirstName + " " + user.LastName, user.UserEmailId, user.UserRole.ToString())
            };

            user.AccessTokenExpiration = jWToken.AccessTokenExpiration = DateTime.Now.AddHours(Codes.Number_2);
            user.RefreshTokenExpiration = jWToken.RefershTokenExpiration = DateTime.Now.AddMonths(Codes.Number_2);
            jWToken.RefershToken = user.RefreshToken = GenerateRefreshToken(
                 user.FirstName + " " + user.LastName,
                user.UserEmailId,
                DateTime.Now.AddMonths(Codes.Number_2).ToString(),
                user.UserRole.ToString()
                );
            HttpContext.Session.SetString(Constants.Token, user.AccessToken);

            // int response = await _userService.UpdateUserInformationAsync(user);

            // if(response > Codes.Number_0)
            {
                return StatusCode(Codes.Number_201, jWToken);
            }

            //  return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);

        }

        [HttpPost]
        [Route("/api/Account/Register")]
        public async Task<IActionResult> Register(UserRegisterModel userCredentials)
        {
            if (userCredentials == null || String.IsNullOrEmpty(userCredentials.EmailAddress))
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            if (await _userService.GetUserByEmailAsync(userCredentials.EmailAddress) != null)
            {
                return StatusCode(Codes.Number_409, Conflict409Error.UserAlreadyExistLogin);
            }

            User user = new User
            {
                UserEmailId = userCredentials.EmailAddress,
                FirstName = userCredentials.FirstName,
                Institution = userCredentials.Institution,
                LastName = userCredentials.LastName,
                Password = userCredentials.Password,
                UserRole = userCredentials.UserRole == Constants.Teacher ? Role.TEACHER : Role.STUDENT
            };

            JWToken jWToken = new JWToken();

            user.AccessToken = jWToken.AccessToken = GenerateAccessToken(
                user.FirstName + " " + user.LastName,
                user.UserEmailId, user.UserRole.ToString());
            user.AccessTokenExpiration = jWToken.AccessTokenExpiration = DateTime.Now.AddHours(Codes.Number_2);
            user.RefreshTokenExpiration = jWToken.RefershTokenExpiration = DateTime.Now.AddMonths(Codes.Number_2);
            user.RefreshToken = jWToken.RefershToken = GenerateRefreshToken(
                user.FirstName + " " + user.LastName,
                user.UserEmailId,
                jWToken.RefershTokenExpiration.ToString(),
                user.UserRole.ToString()
                );
            HttpContext.Session.SetString(Constants.Token, user.AccessToken);

            int response = await _userService.InsertUserAsync(user);

            if (response > Codes.Number_0)
            {
                return StatusCode(Codes.Number_201, jWToken);
            }

            return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var email = ExtractEmailFromJWT();

            var user = await _imageService.GetImageAsync(email);

            return StatusCode(Codes.Number_200, new ProfileUserModel
            {
                Email = email,
                FirstName = user?.User?.FirstName,
                ImageContent = user?.ImageContent,
                ImageExtention = user?.ImageExtention,
                ImageName = user?.ImageName,
                Institution = user?.User?.Institution,
                LastName = user?.User?.LastName,
                Password = user?.User?.Password,
                Role = user?.User?.UserRole.ToString()
            });
        }

        

    }
}