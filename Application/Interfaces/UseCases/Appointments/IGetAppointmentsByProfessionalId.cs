using Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Appointments
{
    public interface IGetAppointmentsByProfessionalId
    {
        Task<IEnumerable<AppointmentDTO>> ExecuteAsync(Guid id);
    }
}
