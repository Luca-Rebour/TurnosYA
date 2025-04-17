using Application.DTOs.Appointment;
using Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> Create(CreateAppointmentDTO appointmentDTO);
        Task<AppointmentDTO> GetById(Guid id);
        Task<AppointmentDTO> Update(Guid id, UpdateAppointmentDTO updateAppointmentDTO);
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByProfessionalId(Guid professionalId);
        Task ConfirmAppointment(Guid id);
        Task CancelExpiredPendingAppointmentsAsync(Guid id);


    }
}
