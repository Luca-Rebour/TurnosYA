using Application.DTOs.Appointment;
using Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<AppointmentDTO>? Appointments { get; set; }
        public ICollection<NotificationDTO>? Notifications { get; set; }
    }

}
