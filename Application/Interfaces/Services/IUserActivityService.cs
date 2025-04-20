using Application.DTOs.UserActivity;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserActivityService
    {
        Task CreateAsync(Guid appointmentId, Guid customerId, Guid professionalId, ActivityType type, string description);
        Task<IEnumerable<UserActivityDTO>> GetAllAsync(Guid userId);
    }
}
