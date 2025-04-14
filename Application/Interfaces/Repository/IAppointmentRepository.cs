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
        Task DeleteAsync(Guid id);

    }
}
