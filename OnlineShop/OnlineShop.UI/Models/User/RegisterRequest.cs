using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.User
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //public string PhoneNumber { get; set; }

        //public string? PersonalNumber { get; set; }

        //[Required]
        //public string Country { get; set; }

        //[Required]
        //public string City { get; set; }

        //[Required]
        //public string Address { get; set; }

        //public DateTime? DateOfBirth { get; set; }

        //public string IdentificationNumber { get; set; }
    }
}