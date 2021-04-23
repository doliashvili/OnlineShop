using System;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
    }
}