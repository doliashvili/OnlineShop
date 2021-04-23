using System;
using System.Linq;
using System.Text.RegularExpressions;
using Exceptions.ThrowHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Application.Settings;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace OnlineShop.Application.Services.Implements
{
    public sealed class FileService : IFileService
    {
        private readonly AppSettings _settings;
        private readonly string _staticFilePath;

        public FileService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _staticFilePath = _settings.StaticFilePath;
        }


        public string CropImage(CropperSetting setting, string additionalFolder = "", string[] allowedExtensions = null)
        {
            var extension = System.IO.Path.GetExtension(setting.Image.FileName).ToLower();
            string filename = System.IO.Path.GetFileNameWithoutExtension(setting.Image.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString("N");
            filename = FixName(filename) + extension;

            var dirPath = System.IO.Path.Combine(_staticFilePath, additionalFolder);
            CreateDirectoryIfNotExists(dirPath);

            var imagePath = System.IO.Path.Combine(additionalFolder, filename);
            var fullPath = System.IO.Path.Combine(_staticFilePath, imagePath);

            using Image image = SixLabors.ImageSharp.Image.Load(setting.Image.OpenReadStream());
            image.Mutate(x => x.Crop(new Rectangle(setting.X, setting.Y, setting.Width, setting.Height)));

            var resizeWidth = 0;
            var resizeHeight = 0;

            if (setting.Width >= setting.Height && setting.Width >= setting.MaxSize)
            {
                var dif = setting.Width / (1.0 * setting.MaxSize);
                resizeWidth = setting.MaxSize;

                resizeHeight = Convert.ToInt32(setting.Height / dif);
            }
            else if (setting.Height > setting.Width && setting.Height >= setting.MaxSize)
            {
                var dif = setting.Height / (1.0 * setting.MaxSize);
                resizeHeight = setting.MaxSize;

                resizeWidth = Convert.ToInt32(setting.Width / dif);
            }

            if (resizeWidth > 0 && resizeHeight > 0)
                image.Mutate(x => x.Resize(resizeWidth, resizeHeight));

            if (extension == ".jpg" || extension == ".jpeg")
            {
                var encoder = new JpegEncoder()
                {
                    Quality = 90,
                };
                image.Save(fullPath, encoder);
            }
            else
            {
                image.Save(fullPath);
            }
            return imagePath;
        }


        public string Image(IFormFile data, string additionalFolder = "", string[] allowedExtensions = null, int maxWidth = 1900)
        {
            Throw.Exception.IfNull(data, () => new ApiException(400, "File can't be null"));
            var extension = System.IO.Path.GetExtension(data.FileName).ToLower();
            Throw.Exception.IfFalse(CheckImageType(extension), $"{extension} is not allowed image extension");
            var filename = System.IO.Path.GetFileNameWithoutExtension(data.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString("N");
            filename = FixName(filename) + extension;

            var dirPath = System.IO.Path.Combine(_staticFilePath, additionalFolder);
            CreateDirectoryIfNotExists(dirPath);

            var imagePath = System.IO.Path.Combine(additionalFolder, filename);
            var fullPath = System.IO.Path.Combine(_staticFilePath, imagePath);

            using var loadedImage = SixLabors.ImageSharp.Image.Load(data.OpenReadStream());
            var width = loadedImage.Width;
            var height = loadedImage.Height;
            var resize = false;

            if (loadedImage.Width >= loadedImage.Height && loadedImage.Width > 1900)
            {
                var dif = loadedImage.Width / 1900.0;
                width = 1900;

                height = Convert.ToInt32(loadedImage.Height / dif);
                resize = true;
            }
            else if (loadedImage.Height > loadedImage.Width && loadedImage.Height > 1900)
            {
                var dif = loadedImage.Height / 1900.0;
                height = 1900;

                width = Convert.ToInt32(loadedImage.Width / dif);
                resize = true;
            }

            if (resize) loadedImage.Mutate(x => x.Resize(width, height));

            if (extension == ".jpg" || extension == ".jpeg")
            {
                var encoder = new JpegEncoder()
                {
                    Quality = 90,
                };
                loadedImage.Save(fullPath, encoder);
            }
            else
            {
                loadedImage.Save(fullPath);
            }

            return imagePath;
        }

        public string File(IFormFile data, string additionalFolder = "", string[] allowedExtensions = null)
        {
            Throw.Exception.IfNull(data, () => new ApiException(400, "File can't be null"));
            var extension = System.IO.Path.GetExtension(data.FileName).ToLower();

            if (allowedExtensions != null)
            {
                var allowedExtension = allowedExtensions.Any(x => string.Equals(x, extension, StringComparison.CurrentCultureIgnoreCase));
                Throw.Exception.IfFalse(allowedExtension, $"{extension} is not allowed extension");
            }

            string filename = System.IO.Path.GetFileNameWithoutExtension(data.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString("N");
            filename = FixName(filename) + extension;

            var dirPath = System.IO.Path.Combine(_staticFilePath, additionalFolder);
            CreateDirectoryIfNotExists(dirPath);

            var filePath = System.IO.Path.Combine(additionalFolder, filename);
            var fullPath = System.IO.Path.Combine(_staticFilePath, filePath);

            using System.IO.FileStream output = System.IO.File.Create(fullPath);
            data.CopyToAsync(output);

            return filePath;
        }

        public bool CheckImageType(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            var ext = System.IO.Path.GetExtension(name);
            return _settings.AllowedImageExtensions.Contains(ext);
        }

        private string FixName(string name)
        {
            name = Regex.Replace(name, @"[^a-zA-Zá-á°0-9\s-]", "-");
            name = Regex.Replace(name, @"\s+", " ").Trim();
            name = name.Substring(0, name.Length <= 100 ? name.Length : 100).Trim();
            name = Regex.Replace(name, @"\s", "-");
            name = Regex.Replace(name, @"\-+", "-").Trim();
            name = Regex.Replace(name, @"\-$", "").Trim();

            return name;
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            var exists = System.IO.Directory.Exists(path);

            if (!exists)
                System.IO.Directory.CreateDirectory(path);
        }
    }
}
