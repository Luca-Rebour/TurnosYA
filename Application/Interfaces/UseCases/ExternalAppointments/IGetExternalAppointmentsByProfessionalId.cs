using Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Appointments
{
    public interface IGetExternalAppointmentsByProfessionalId
    {
        Task<IEnumerable<ExternalAppointmentDTO>> ExecuteAsync(Guid id);
    }
}
