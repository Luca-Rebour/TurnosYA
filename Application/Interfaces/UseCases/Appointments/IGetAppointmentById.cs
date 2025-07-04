using Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Appointments
{
    public interface IGetAppointmentById
    {
        Task<AppointmentDTO> ExecuteAsync(Guid id);
    }
}
