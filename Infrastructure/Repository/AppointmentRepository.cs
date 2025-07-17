using Application.Interfaces.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public void UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }
        public async Task<Appointment> GetByIdAsync(Guid id)
        {
            Appointment appointment = await _context.Appointments.FindAsync(id);
            return appointment;
        }
    }
}
