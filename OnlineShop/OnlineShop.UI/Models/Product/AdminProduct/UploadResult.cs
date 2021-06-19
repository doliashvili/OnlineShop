using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Product.AdminProduct
{
    public class UploadResult
    {
        public string FileName { get; set; }
        public int ErrorCode { get; set; }
        public bool Uploaded { get; set; }
        public string StoredFileName { get; set; }
        public string ImageUrl { get; set; }
        public bool MainImage { get; set; }

        //Todo დროებითია სერვერზე რო აიწევა მარტო imageUrl წავა
        public string ImagePath => "http://127.0.0.1:8887/images" + "/" + StoredFileName;
    }
}