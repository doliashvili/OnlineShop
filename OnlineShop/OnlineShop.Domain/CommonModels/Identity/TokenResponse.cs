using System;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string RefreshToken { get; set; }
    }
}