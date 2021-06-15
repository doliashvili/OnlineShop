using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        [HttpPost("upload-files")]
        public async Task<ActionResult<IList<UploadResult>>> PostFileAsync(
         IEnumerable<IFormFile> files)
        {
            var maxAllowedFiles = 3;
            long maxFileSize = 1024 * 1024 * 15;
            var filesProcessed = 0;
            //var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<UploadResult> uploadResults = new(3);

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            var trustedFileNameForFileStorage = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                            var path = Path.Combine(_settings.StaticFilePath,
                                trustedFileNameForFileStorage);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                            uploadResult.ImageUrl = path;
                        }
                        catch (IOException ex)
                        {
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }

            return uploadResults;
        }

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

    public class UploadResult
    {
        public string FileName { get; set; }
        public int ErrorCode { get; set; }
        public bool Uploaded { get; set; }
        public string StoredFileName { get; set; }
        public string ImageUrl { get; set; }
    }
}