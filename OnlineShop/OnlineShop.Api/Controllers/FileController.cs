using Helpers.ReadResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Application.Settings;

namespace OnlineShop.Api.Controllers
{
    [Route("api/v1/file")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly AppSettings _settings;

        public FileController(IFileService fileService, AppSettings settings)
        {
            _fileService = fileService;
            _settings = settings;
        }

/*
        [HttpPost("upload-file")]
        public IActionResult File([FromForm] IFormFile file, [FromForm] string path = "")
        {
            var filePath = _fileService.File(file, path);
            return Ok(Result<string>.Success(filePath));
        }
*/
        

        [AllowAnonymous]
        [HttpPost("upload-image")]
        public IActionResult Image([FromForm] IFormFile file)
        {
            var imagePath = _fileService.Image(file, "images");
            return Ok(Result<string>.Success(imagePath));
        }

        [RequestSizeLimit(52428800)]
        [AllowAnonymous]
        [HttpPost("upload-video")]
        public IActionResult Video([FromForm] IFormFile video)
        {
            if (video.Length > 52428800)
                return BadRequest(Result<string>.Fail("allowed file-size is 50mb."));
            
            var imagePath = _fileService.File(video, "video", _settings.AllowedVideoExtensions);
            return Ok(Result<string>.Success(imagePath));
        }
        
        
        [AllowAnonymous]
        [HttpPost("crop-image")]
        public IActionResult CropImage([FromForm] CropperSetting cropperData, [FromForm] string path = "")
        {
            var imagePath = _fileService.CropImage(cropperData, path);
            return Ok(Result<string>.Success(imagePath));
        }

    }
}
