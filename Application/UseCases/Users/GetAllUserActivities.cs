using Application.DTOs.UserActivity;
using Application.Interfaces.Repository;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Entities;


namespace Application.UseCases.User
{
    public class GetAllUserActivities : IGetAllUserActivities
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private IMapper _mapper;
        public GetAllUserActivities(IUserActivityRepository userActivityRepository, IMapper mapper)
        {
            _userActivityRepository = userActivityRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserActivityDTO>> ExecuteAsync(Guid userId)
        {
            IEnumerable<UserActivity> activities = await _userActivityRepository.GetAllAsync(userId);
            IEnumerable<UserActivityDTO> ret = _mapper.Map<IEnumerable<UserActivityDTO>>(activities);
            return ret;

        }
    }
}
