using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PersonalNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string? IdentificationNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public RefreshToken? RefreshToken { get; set; }
        public DateTime? ActivatedAt { get; set; }

        public ApplicationUser(string firstName, string lastName, DateTime createdAt, DateTime? dateOfBirth, string? personalNumber, string country, string city, string address, string? identificationNumber, bool isActive, RefreshToken? refreshToken, DateTime? activatedAt)
        {
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = createdAt;
            DateOfBirth = dateOfBirth;
            PersonalNumber = personalNumber;
            Country = country;
            City = city;
            Address = address;
            IdentificationNumber = identificationNumber;
            IsActive = isActive;
            RefreshToken = refreshToken;
            ActivatedAt = activatedAt;
        }

        public ApplicationUser()
        {
        }
    }
}