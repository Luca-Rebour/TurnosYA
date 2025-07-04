using Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Notifications
{
    public interface ICreateNotification
    {
        Task<NotificationDTO> Create(NotificationDTO notificationDTO);
    }
}
