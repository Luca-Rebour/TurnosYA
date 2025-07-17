using Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Appointments
{
    public interface IUpdateInternalAppointment
    {
        Task<InternalAppointmentDTO> ExecuteAsync(Guid id, UpdateInternalAppointmentDTO updateAppointmentDTO);
    }
}
