using Application.DTOs.Customer;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Appointment> AddAsync(Appointment newAppointment)
        {
            await _context.Appointments.AddAsync(newAppointment);
            await _context.SaveChangesAsync();
            return newAppointment;
        }


        public async Task DeleteAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Appointment> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Professional)
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
        }


        public async Task<Appointment> UpdateAsync(Guid id, Appointment updatedAppointment)
        {
            Appointment existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
                return null;

            existingAppointment.Update(updatedAppointment);

            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByProfessionalId(Guid professionalId)
        {
            IEnumerable<Appointment> appointments = await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Professional)
                .Where(a => a.ProfessionalId == professionalId)
                .ToListAsync();

            return appointments;
        }


        public async Task<IEnumerable<Customer>> GetCustomersByProfessionalId(Guid professionalId)
        {
            IEnumerable<Customer> customers = await _context.Appointments
                .Where(a => a.ProfessionalId == professionalId)
                .Select(a => a.Customer)
                .Distinct()
                .ToListAsync();

            return customers;


        }


        public async Task<IEnumerable<Appointment>> GetPendingAppointmentsByUserIdAsync(Guid id)
        {
            return await _context.Appointments
                .Where(a => (a.ProfessionalId == id || a.CustomerId == id) && a.Status == 0)
                .ToListAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
