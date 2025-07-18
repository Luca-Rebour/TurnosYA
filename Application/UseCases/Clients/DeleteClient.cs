using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Clients;

namespace Application.UseCases.Clients
{
    public class DeleteClient: IDeleteClient
    {
        private IClientRepository _repository;

        public DeleteClient(IClientRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
