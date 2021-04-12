using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Application.Settings
{
    public class CropperSetting
    {
        public CropperSetting()
        {
            MaxSize = 1900;
        }
        [Required]
        public IFormFile Image { get; set; }
        public string FieldName { get; set; }
        public string Format { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ResizeWidth { get; set; }
        public int MaxSize { get; set; }
        public int Rotate { get; set; }
    }
}
