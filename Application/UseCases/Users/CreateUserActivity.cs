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
        public async Task ExecuteAsync(Guid appointmentId, Guid customerId, Guid professionalId, ActivityType type, string description)
        {
            UserActivity act = new UserActivity(appointmentId, customerId, professionalId, type, description, null, null, null);
            await _userActivityRepository.AddAsync(act);
        }
    }
}
