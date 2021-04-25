﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.CommonModels.Identity
{
    public sealed class RegisterRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }
        
        public string? PersonalNumber { get; set; }
        
        public string? Country { get; set; }
        
        public string? City { get; set; }

        public string? Address { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string IdentificationNumber { get; set; }

        public RegisterRequest(string? firstName, string? lastName, string email, string password, string confirmPassword, string? phoneNumber, string? personalNumber, string? country, string? city, string? address, DateTime? dateOfBirth, string identificationNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            PhoneNumber = phoneNumber;
            PersonalNumber = personalNumber;
            Country = country;
            City = city;
            Address = address;
            DateOfBirth = dateOfBirth;
            IdentificationNumber = identificationNumber;
        }
    }
}