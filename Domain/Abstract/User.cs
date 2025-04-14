using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public abstract class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PasswordHash { get; private set; }
        public ICollection<Appointment> Appointments { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public ICollection<Notification> Notifications { get; private set; }
        public User()
        {
            Appointments = new List<Appointment>();
            Notifications = new List<Notification>();
            RegistrationDate = DateTime.UtcNow;
        }

        public User(string name, string lastName, string email, string phone, DateTime birthDate, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            PasswordHash = passwordHash;
            Notifications = new List<Notification>();
            Appointments = new List<Appointment>();
            RegistrationDate = DateTime.UtcNow;
        }

        public void UpdateUser(User other)
        {
            if (other == null)
                return;

            if (!string.IsNullOrWhiteSpace(other.Name))
                Name = other.Name;

            if (!string.IsNullOrWhiteSpace(other.LastName))
                LastName = other.LastName;

            if (!string.IsNullOrWhiteSpace(other.Phone))
                Phone = other.Phone;

            if (other.BirthDate != default && other.BirthDate != BirthDate)
                BirthDate = other.BirthDate;

            if (!string.IsNullOrWhiteSpace(other.PasswordHash))
                PasswordHash = other.PasswordHash;
        }

        public void SetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }


    }
}
