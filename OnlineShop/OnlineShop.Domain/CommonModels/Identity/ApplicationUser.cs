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
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = false;
        public RefreshToken? RefreshToken { get; set; }
        public DateTime? ActivatedAt { get; set; }

        public ApplicationUser(string email, string firstName, string lastName, DateTime createdAt, bool isActive, RefreshToken? refreshToken, DateTime? activatedAt, string? phoneNumber)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = createdAt;
            IsActive = isActive;
            RefreshToken = refreshToken;
            ActivatedAt = activatedAt;
        }

        public void UpdateUserInfoForOrder(DateTime? dateOfBirth, string country, string city,
            string address, string phoneNumber)
        {
            DateOfBirth = dateOfBirth;
            Country = country;
            City = city;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public ApplicationUser()
        {
        }
    }
}