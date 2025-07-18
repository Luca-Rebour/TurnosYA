using Aplication.Interfaces.Repository;
using Application.Interfaces.Repository;
using Application.Interfaces.Users;
using Domain.Entities;
using Domain.Enums;


namespace Application.UseCases.User
{
    public class CreateUserActivity : ICreateUserActivity
    {
        private readonly IUserActivityRepository _userActivityRepository;
        public CreateUserActivity(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }
        public async Task ExecuteAsync(Guid appointmentId, Guid clientId, Guid professionalId, ActivityType type, string description)
        {
            UserActivity act = new UserActivity(appointmentId, clientId, professionalId, type, description, null, null, null);
            await _userActivityRepository.AddAsync(act);
        }
    }
}
