using Application.DTOs.Client;
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
    public class ExternalAppointmentRepository : IExternalAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ExternalAppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ExternalAppointment> AddAsync(ExternalAppointment newAppointment)
        {
            await _context.ExternalAppointments.AddAsync(newAppointment);
            await _context.SaveChangesAsync();
            return newAppointment;
        }


        public async Task DeleteAsync(Guid id)
        {
            var appointment = await _context.ExternalAppointments.FindAsync(id);
            if (appointment != null)
            {
                _context.ExternalAppointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<ExternalAppointment> GetByIdAsync(Guid id)
        {
            return await _context.ExternalAppointments
                .Include(a => a.ExternalClient)
                .Include(a => a.Professional)
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
        }


        public async Task<ExternalAppointment> UpdateAsync(Guid id, ExternalAppointment updatedAppointment)
        {
            ExternalAppointment existingAppointment = await _context.ExternalAppointments.FindAsync(id);
            if (existingAppointment == null)
                return null;

            existingAppointment.Update(updatedAppointment);

            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<IEnumerable<ExternalAppointment>> GetExternalAppointmentsByProfessionalId(Guid professionalId)
        {
            IEnumerable<ExternalAppointment> appointments = await _context.ExternalAppointments
                .Include(a => a.ExternalClient)
                .Include(a => a.Professional)
                .Where(a => a.ProfessionalId == professionalId)
                .ToListAsync();

            return appointments;
        }


        public async Task<IEnumerable<ExternalClient>> GetClientsByProfessionalId(Guid professionalId)
        {
            IEnumerable<ExternalClient> clients = await _context.ExternalAppointments
                .Where(a => a.ProfessionalId == professionalId)
                .Select(a => a.ExternalClient)
                .Distinct()
                .ToListAsync();

            return clients;


        }


        public async Task<IEnumerable<ExternalAppointment>> GetPendingExternalAppointmentsByUserIdAsync(Guid id)
        {
            return await _context.ExternalAppointments
                .Where(a => (a.ProfessionalId == id || a.ExternalClientId == id) && a.Status == 0)
                .ToListAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExternalAppointment>> GetThisWeekExternalAppointmentsByIdAsync(Guid id)
        {
            var today = DateTime.Today;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            var weekEnd = weekStart.AddDays(7);

            return await _context.ExternalAppointments
                .Where(a => a.ProfessionalId == id && a.Date >= weekStart && a.Date < weekEnd)
                .ToListAsync();
        }
    }
}
