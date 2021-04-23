
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public class TokenRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}