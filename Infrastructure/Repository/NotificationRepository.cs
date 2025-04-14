using Application.Interfaces.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Notification> AddAsync(Notification newNotification)
        {
            _context.Notifications.AddAsync(newNotification);
            await _context.SaveChangesAsync();
            return newNotification;
        }

        public async Task DeleteAsync(Guid id)
        {
            Notification notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
            }
            await _context.SaveChangesAsync();
            
        }

        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            Notification notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                throw new Exception("Notification not found");
            }
            return notification;
        }
    }
}
