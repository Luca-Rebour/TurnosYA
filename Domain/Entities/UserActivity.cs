using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserActivity
    {
        public Guid Id { get; private set; }
        public Guid AppointmentId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProfessionalId { get; private set; }
        public ActivityType Type { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Appointment? Appointment { get; private set; }
        public Customer? Customer { get; private set; }
        public Professional? Professional { get; private set; }

        public UserActivity() { }
        public UserActivity(Guid appointmentId, Guid customerId, Guid professionalId, ActivityType type, string? description, Appointment? appointment, Customer? customer, Professional? professional)
        {
            Id = new Guid();
            AppointmentId = appointmentId;
            CustomerId = customerId;
            ProfessionalId = professionalId;
            Type = type;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            Appointment = appointment;
            Customer = customer;
            Professional = professional;
        }
    }

}
