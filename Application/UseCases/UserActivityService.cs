using Application.DTOs.UserActivity;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IMapper _mapper;
        public UserActivityService(IUserActivityRepository userActivityRepository, IMapper mapper)
        {
            _userActivityRepository = userActivityRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(Guid appointmentId, Guid customerId, Guid professionalId, ActivityType type, string description)
        {
            UserActivity act = new UserActivity(appointmentId, customerId, professionalId, type, description, null, null, null);
            await _userActivityRepository.AddAsync(act);
        }

        public async Task<IEnumerable<UserActivityDTO>> GetAllAsync(Guid userId)
        {
            IEnumerable<UserActivity> activities = await _userActivityRepository.GetAllAsync(userId);
            IEnumerable<UserActivityDTO> ret = _mapper.Map<IEnumerable<UserActivityDTO>>(activities);
            return ret;
            
        }
    }
}
