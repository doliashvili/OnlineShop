using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public sealed class AddPersonalInfoRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        //public string? PersonalNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public AddPersonalInfoRequest(string phoneNumber, string country, string city, string address, DateTime dateOfBirth)
        {
            PhoneNumber = phoneNumber;
            Country = country;
            City = city;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
    }
}