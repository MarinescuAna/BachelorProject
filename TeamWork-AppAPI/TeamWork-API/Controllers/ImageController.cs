using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Account.Domain;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork_API.ErrorHandler;
using TeamWork_API.Utils;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        public ImageController(IConfiguration configuration, IUserService userService, IImageService imageService, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
            _userService = userService;
            _imageService = imageService;
        }

        [HttpPost]
        [Route("InsertUserImage")]
        public async Task<IActionResult> InsertUserImage(UploadImageModel uploadImageModel)
        {
            if (uploadImageModel == null)
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            var existentImage = await _imageService.GetImageAsync(ExtractEmailFromJWT());
            if (existentImage != null)
            {
                existentImage.ImageContent = uploadImageModel.ImageContent;
                existentImage.ImageExtention = uploadImageModel.ImageExtention;
                existentImage.ImageName = uploadImageModel.ImageName;

                if (await _imageService.UpdateImageAsync(existentImage))
                {
                    return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
                }

                return Ok();
            }

            if (await _imageService.InsertImageAsync(new Image
            {
                ImageContent = uploadImageModel.ImageContent,
                UserId = ExtractEmailFromJWT(),
                ImageExtention = uploadImageModel.ImageExtention,
                ImageName = uploadImageModel.ImageName,
                ImageId = Guid.NewGuid()
            }))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }
            return Ok();

        }
    }
}
