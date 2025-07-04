using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Customers;

namespace Application.UseCases.Customers
{
    public class DeleteCustomer: IDeleteCustomer
    {
        private ICustomerRepository _repository;

        public DeleteCustomer(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
