using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}