using Helpers.ReadResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Services;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Application.Settings;

namespace OnlineShop.Api.Controllers
{
    [ApiController]
    [Route("api/v1/file")]
    [Authorize(Roles = "Moderator,Admin")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly AppSettings _settings;

        public FileController(IFileService fileService, IOptions<AppSettings> settings)
        {
            _fileService = fileService;
            _settings = settings.Value;
        }

        /*
                [HttpPost("upload-file")]
                public IActionResult File([FromForm] IFormFile file, [FromForm] string path = "")
                {
                    var filePath = _fileService.File(file, path);
                    return Ok(Result<string>.Success(filePath));
                }
        */

        [HttpPost("upload-image")]
        public IActionResult Image(IFormFile file)
        {
            var imagePath = _fileService.Image(file, "images");
            return Ok(Result<string>.Success(imagePath));
        }

        [RequestSizeLimit(52428800)]
        [HttpPost("upload-video")]
        public IActionResult Video(IFormFile video)
        {
            if (video.Length > 52428800)
                return BadRequest(Result<string>.Fail("allowed file-size is 50mb."));

            var imagePath = _fileService.File(video, "video", _settings.AllowedVideoExtensions);
            return Ok(Result<string>.Success(imagePath));
        }

        [HttpPost("crop-image")]
        public IActionResult CropImage(CropperSetting cropperData, string path = "")
        {
            var imagePath = _fileService.CropImage(cropperData, path);
            return Ok(Result<string>.Success(imagePath));
        }
    }
}