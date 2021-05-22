using System;

namespace OnlineShop.UI.Models.User
{
    public class LoginResponse
    {
        public Result Result { get; set; }

        public Token Token { get; set; }

        public string UserId { get; set; }
    }

    public class Result
    {
        public bool RequiresTwoFactor { get; set; }

        public bool Succeeded { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsNotAllowed { get; set; }
    }

    public class Token
    {
        public string TokenValue { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public string RefreshToken { get; set; }
    }
}