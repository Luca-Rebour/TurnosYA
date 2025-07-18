using Application.DTOs.Client;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IExternalAppointmentRepository
    {
        Task<ExternalAppointment> AddAsync(ExternalAppointment newAppointment);
        Task<ExternalAppointment> UpdateAsync(Guid id, ExternalAppointment updatedAppointment);
        Task<ExternalAppointment> GetByIdAsync(Guid id);
        Task<IEnumerable<ExternalAppointment>> GetExternalAppointmentsByProfessionalId(Guid professionalId);
        Task<IEnumerable<ExternalClient>> GetClientsByProfessionalId(Guid professionalId);
        Task<IEnumerable<ExternalAppointment>> GetThisWeekExternalAppointmentsByIdAsync(Guid id);
        Task<IEnumerable<ExternalAppointment>> GetPendingExternalAppointmentsByUserIdAsync(Guid id);
        Task SaveChangesAsync();
        Task DeleteAsync(Guid id);

    }
}
