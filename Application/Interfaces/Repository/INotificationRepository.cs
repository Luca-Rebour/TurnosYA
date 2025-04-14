using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface INotificationRepository
    {
        public Task<Notification> AddAsync(Notification newNotification);
        public Task<Notification?> GetByIdAsync(Guid id);
        public Task DeleteAsync(Guid id);
    }
}
