using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IUserActivityRepository
    {
        Task AddAsync(UserActivity activity);
        Task<IEnumerable<UserActivity>> GetAllAsync(Guid userId);
    }
}
