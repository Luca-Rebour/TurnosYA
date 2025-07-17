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
    public class InternalAppointmentRepository : IInternalAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public InternalAppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<InternalAppointment> AddAsync(InternalAppointment newAppointment)
        {
            await _context.InternalAppointments.AddAsync(newAppointment);
            await _context.SaveChangesAsync();
            return newAppointment;
        }


        public async Task DeleteAsync(Guid id)
        {
            var appointment = await _context.InternalAppointments.FindAsync(id);
            if (appointment != null)
            {
                _context.InternalAppointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<InternalAppointment> GetByIdAsync(Guid id)
        {
            return await _context.InternalAppointments
                .Include(a => a.Customer)
                .Include(a => a.Professional)
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
        }


        public async Task<InternalAppointment> UpdateAsync(Guid id, InternalAppointment updatedAppointment)
        {
            InternalAppointment existingAppointment = await _context.InternalAppointments.FindAsync(id);
            if (existingAppointment == null)
                return null;

            existingAppointment.Update(updatedAppointment);

            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<IEnumerable<InternalAppointment>> GetInternalAppointmentsByProfessionalId(Guid professionalId)
        {
            IEnumerable<InternalAppointment> appointments = await _context.InternalAppointments
                .Include(a => a.Customer)
                .Include(a => a.Professional)
                .Where(a => a.ProfessionalId == professionalId)
                .ToListAsync();

            return appointments;
        }


        public async Task<IEnumerable<Customer>> GetCustomersByProfessionalId(Guid professionalId)
        {
            IEnumerable<Customer> customers = await _context.InternalAppointments
                .Where(a => a.ProfessionalId == professionalId)
                .Select(a => a.Customer)
                .Distinct()
                .ToListAsync();

            return customers;


        }


        public async Task<IEnumerable<InternalAppointment>> GetPendingInternalAppointmentsByUserIdAsync(Guid id)
        {
            return await _context.InternalAppointments
                .Where(a => (a.ProfessionalId == id || a.CustomerId == id) && a.Status == 0)
                .ToListAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InternalAppointment>> GetThisWeekInternalAppointmentsByIdAsync(Guid id)
        {
            var today = DateTime.Today;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            var weekEnd = weekStart.AddDays(7);

            return await _context.InternalAppointments
                .Where(a => a.ProfessionalId == id && a.Date >= weekStart && a.Date < weekEnd)
                .ToListAsync();
        }


    }
}
