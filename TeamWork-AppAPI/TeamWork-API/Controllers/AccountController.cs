using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.AccountDTO;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.Common.Enums;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.Common.ConstantStrings;
using TeamWork_API.Factory;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly TokenGeneratorHelper _tokenGeneratorHelper;
        private readonly ImageHelper _imagerHelper;
        private readonly SecurityHelper _securityHelper;
        public AccountController(
            IHelperFactory factoryHelper,
            IUserService userService,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _imagerHelper = factoryHelper.CreateImageHelper();
            _tokenGeneratorHelper = factoryHelper.CreateTokenGeneratorHelper();
            _userService = userService;
            _imageService = imageService;
            _securityHelper = factoryHelper.CreateSecurityHelper();
        }

        [HttpPost]
        [Route("/api/Account/Login")]
        public async Task<IActionResult> Login(UserLoginModel credentials)
        {
            if (credentials == null || String.IsNullOrEmpty(credentials.EmailAddress))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            User user = await _userService.GetUserByEmailAsync(credentials.EmailAddress);

            if (user == null)
            {
                return StatusCode(Number.Number_404, NotFound404Error.InvalidEmail);
            }
            var userPassowrd = _securityHelper.DecryptString(user.Password);
            if(string.IsNullOrEmpty(userPassowrd))
            {
                return StatusCode(Number.Number_404, NotFound404Error.DecryptionError);
            }

            if (credentials.Password != userPassowrd)
            {
                return StatusCode(Number.Number_404, NotFound404Error.InvalidPassword);
            }

            var jWToken = _tokenGeneratorHelper.GenerateTokenAndSaveTokensInUser(ref user);
            HttpContext.Session.SetString(Constants.Token, user.AccessToken);

            if(await _userService.UpdateUserInformationAsync(user) > Number.Number_0)
            {
                return StatusCode(Number.Number_201, jWToken);
            }

            return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);

        }

        [HttpPost]
        [Route("/api/Account/Register")]
        public async Task<IActionResult> Register(UserRegisterModel userCredentials)
        {
            if (userCredentials == null || String.IsNullOrEmpty(userCredentials.EmailAddress))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (await _userService.GetUserByEmailAsync(userCredentials.EmailAddress) != null)
            {
                return StatusCode(Number.Number_409, Conflict409Error.UserAlreadyExistLogin);
            }

            User user = new User
            {
                UserEmailId = userCredentials.EmailAddress,
                FirstName = userCredentials.FirstName,
                Institution = userCredentials.Institution,
                LastName = userCredentials.LastName,
                Password = _securityHelper.EncryptString(userCredentials.Password),
                UserRole = userCredentials.UserRole == Constants.Teacher ? Role.TEACHER : Role.STUDENT
            };

            var jWToken = _tokenGeneratorHelper.GenerateTokenAndSaveTokensInUser(ref user);
            HttpContext.Session.SetString(Constants.Token, user.AccessToken);

            if (await _userService.InsertUserAsync(user) > Number.Number_0)
            {
                return StatusCode(Number.Number_201, jWToken);
            }

            return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
               email =  ExtractEmailFromJWT();
            }

            if (string.IsNullOrEmpty(email))
            {
                return StatusCode(Number.Number_401, Unauthorized401Error.Unauthorized);
            }

            var user = await _imageService.GetImageAsync(email);
            if (user != null)
            {
                if (user.User == null)
                {
                    user.User = await _userService.GetUserByEmailAsync(email);
                }
                return StatusCode(Number.Number_200, new ProfileUserModel
                {
                    Email = email,
                    FirstName = user?.User?.FirstName,
                    ImageContent = _imagerHelper.DecompressImage(user?.ImageContent),
                    ImageExtention = user?.ImageExtention,
                    Institution = user?.User?.Institution,
                    LastName = user?.User?.LastName,
                    Role = user?.User?.UserRole.ToString()
                });
            }
            else
            {
                var user2 = await _userService.GetUserByEmailAsync(email);
                return StatusCode(Number.Number_200, new ProfileUserModel
                {
                    Email = email,
                    FirstName = user2.FirstName,
                    Institution = user2.Institution,
                    LastName = user2.LastName,
                    Role = user2.UserRole.ToString()
                });
            }
        }


        [HttpPut]
        [Authorize]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(ProfileUserModel user)
        {
            if (user == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var oldUser = await _userService.GetUserByEmailAsync(user.Email);
            if (oldUser == null)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            oldUser.FirstName = string.IsNullOrEmpty(user.FirstName) ? oldUser.FirstName : user.FirstName;
            oldUser.LastName = string.IsNullOrEmpty(user.LastName) ? oldUser.LastName : user.LastName;
            oldUser.Institution = string.IsNullOrEmpty(user.Institution) ? oldUser.Institution : user.Institution;

            if (await _userService.UpdateUserInformationAsync(oldUser) == 0)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordChanger password)
        {
            if (password == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var oldUser =await _userService.GetUserByEmailAsync(ExtractEmailFromJWT());
            var passwordDecrypt = _securityHelper.DecryptString(oldUser.Password);

            if (string.IsNullOrEmpty(passwordDecrypt))
            {
                return StatusCode(Number.Number_404, NotFound404Error.DecryptionError);
            }

            if ( passwordDecrypt != password.OldPassword)
            {
                return StatusCode(Number.Number_409, Conflict409Error.PasswordDontMach);
            }
            oldUser.Password = _securityHelper.EncryptString(password.NewPassword);

            if (await _userService.UpdateUserInformationAsync(oldUser) == 0)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }


    }
}