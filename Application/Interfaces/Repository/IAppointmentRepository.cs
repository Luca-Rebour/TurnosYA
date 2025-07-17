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
        void UpdateAsync(Appointment appointment);
        Task<Appointment> GetByIdAsync(Guid id);
    }
}
