using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Settings;

namespace OnlineShop.Application.Services
{
    public interface IFileService
    {
        public string CropImage(CropperSetting setting, string additionalFolder = "",
            string[] allowedExtensions = null);

        public string Image(IFormFile data, string additionalFolder = "", string[] allowedExtensions = null,
            int maxWidth = 1900);

        public string File(IFormFile data, string additionalFolder = "", string[] allowedExtensions = null);

        public bool CheckImageType(string name);
    }
}
