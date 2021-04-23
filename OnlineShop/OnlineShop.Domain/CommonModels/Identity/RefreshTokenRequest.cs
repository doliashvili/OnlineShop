using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public class RefreshTokenRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
