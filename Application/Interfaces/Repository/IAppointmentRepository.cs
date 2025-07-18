using Application.DTOs.Client;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment newAppointment);
        Task<Appointment> UpdateAsync(Guid id, Appointment updatedAppointment);
        Task<Appointment> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAppointmentsByProfessionalId(Guid professionalId);
        Task<IEnumerable<Client>> GetClientsByProfessionalId(Guid professionalId);
        Task<IEnumerable<Appointment>> GetPendingAppointmentsByUserIdAsync(Guid id);
        Task SaveChangesAsync();
        Task DeleteAsync(Guid id);

    }
}
