using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notification
{
    public class NotificationDTO
    {
        public Guid UserId { get; private set; }
        public string Message { get; private set; }
    }
}
