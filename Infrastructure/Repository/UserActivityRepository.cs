using Application.Interfaces.Repository;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public UserActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserActivity activity)
        {
            await _context.AddAsync(activity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<UserActivity>> GetAllAsync(Guid userId)
        {
            IEnumerable<UserActivity> activities = await _context.UserActivities
                .Include(a => a.Client)
                .Include(a => a.Professional)
                .Where(a => a.ClientId == userId || a.ProfessionalId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return activities;

        }
    }
}
