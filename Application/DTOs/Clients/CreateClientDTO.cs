using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.DTOs.Client
{
    public class CreateClientDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; } // Luego se hashea al guardarse



        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Name is required.");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ValidationException("Last name is required.");

            if (string.IsNullOrWhiteSpace(Email))
                throw new ValidationException("Email is required.");

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ValidationException("Email format is invalid.");

            if (string.IsNullOrWhiteSpace(Phone))
                throw new ValidationException("Phone number is required.");

            if (!Regex.IsMatch(Phone, @"^\+?\d{7,15}$"))
                throw new ValidationException("Phone number format is invalid.");

            if (BirthDate == default)
                throw new ValidationException("Birth date is required.");

            if (BirthDate > DateTime.UtcNow.Date)
                throw new ValidationException("Birth date cannot be in the future.");

            if (BirthDate > DateTime.UtcNow.AddYears(-18))
                throw new ValidationException("User must be at least 18 years old.");

            if (string.IsNullOrWhiteSpace(Password))
                throw new ValidationException("Password is required.");

            if (Password.Length < 6)
                throw new ValidationException("Password must be at least 6 characters long.");
        }



    }
}
