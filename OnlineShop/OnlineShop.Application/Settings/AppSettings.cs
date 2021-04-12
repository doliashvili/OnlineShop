namespace OnlineShop.Application.Settings
{
    public sealed class AppSettings
    {
        public string StaticFilePath { get; set; }
        public string LogsPath { get; set; }
        
        public string DisplayPath { get; set; }
        public string[] AllowedImageExtensions { get; set; }
        public string[] AllowedVideoExtensions { get; set; }
        public string ConfirmEmailHtml { get; set; }
        public string ResetPasswordHtml { get; set; }
        public string ConfirmEmailSubject { get; set; }
        public string ResetTokenSubject { get; set; }
        public string ConfirmEmailPath { get; set; }
    }
}
