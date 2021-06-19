namespace OnlineShop.Api.Models
{
    public class UploadResult
    {
        public string FileName { get; set; }
        public int ErrorCode { get; set; }
        public bool Uploaded { get; set; }
        public string StoredFileName { get; set; }
        public string ImageUrl { get; set; }
    }
}