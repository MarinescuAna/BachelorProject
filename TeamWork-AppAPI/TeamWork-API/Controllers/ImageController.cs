using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;
        public ImageController(IConfiguration configuration,IImageService imageService, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
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
                existentImage.ImageContent = CompressImage(uploadImageModel.ImageContent);
                existentImage.ImageExtention = uploadImageModel.ImageExtention;

                if (!(await _imageService.UpdateImageAsync(existentImage)))
                {
                    return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
                }

                return Ok();
            }

            if (!(await _imageService.InsertImageAsync(new Image
            {
                ImageContent = CompressImage(uploadImageModel.ImageContent),
                UserId = ExtractEmailFromJWT(),
                ImageExtention = uploadImageModel.ImageExtention,
                ImageId = Guid.NewGuid()
            })))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }
            return Ok();

        }

        private string CompressImage(string strBase64)
        {
            if (string.IsNullOrEmpty(strBase64))
            {
                return string.Empty;
            }

            Chilkat.Compression compress = new Chilkat.Compression
            {
                Algorithm = "deflate"
            };

            Chilkat.BinData binDat = new Chilkat.BinData();
            // Load the base64 data into a BinData object.
            // This decodes the base64. The decoded bytes will be contained in the BinData.
            binDat.AppendEncoded(strBase64, "base64");

            // Compress the BinData.
            compress.CompressBd(binDat);

            // Get the compressed data in base64 format:
            return binDat.GetEncoded("base64");
        }
    }
}
