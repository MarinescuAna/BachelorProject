using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.DataAccess.Domain.AccountDTO;
using TeamWork.DataAccess.Domain.Models;
using TeamWork_API.Utils;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("InsertUserImage")]
        public async Task<IActionResult> InsertUserImage(UploadImageModel uploadImageModel)
        {
            if (uploadImageModel == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var existentImage = await _imageService.GetImageAsync(ExtractEmailFromJWT());
            if (existentImage != null)
            {
                existentImage.ImageContent = ImageProcessing.CompressImage(uploadImageModel.ImageContent);
                existentImage.ImageExtention = uploadImageModel.ImageExtention;

                if (!await _imageService.UpdateImageAsync(existentImage))
                {
                    return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
                }

                return Ok();
            }

            if (!await _imageService.InsertImageAsync(new Image
            {
                ImageContent = ImageProcessing.CompressImage(uploadImageModel.ImageContent),
                UserId = ExtractEmailFromJWT(),
                ImageExtention = uploadImageModel.ImageExtention,
                ImageId = Guid.NewGuid()
            }))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }
            return Ok();

        }
    }
}
