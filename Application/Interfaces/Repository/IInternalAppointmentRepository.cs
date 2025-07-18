using Application.DTOs.Client;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IInternalAppointmentRepository
    {
        Task<InternalAppointment> AddAsync(InternalAppointment newAppointment);
        Task<InternalAppointment> UpdateAsync(Guid id, InternalAppointment updatedAppointment);
        Task<InternalAppointment> GetByIdAsync(Guid id);
        Task<IEnumerable<InternalAppointment>> GetInternalAppointmentsByProfessionalId(Guid professionalId);
        Task<IEnumerable<Client>> GetClientsByProfessionalId(Guid professionalId);
        Task<IEnumerable<InternalAppointment>> GetThisWeekInternalAppointmentsByIdAsync(Guid id);
        Task<IEnumerable<InternalAppointment>> GetPendingInternalAppointmentsByUserIdAsync(Guid id);
        Task SaveChangesAsync();
        Task DeleteAsync(Guid id);

    }
}
