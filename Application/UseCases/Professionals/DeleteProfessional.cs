using Aplication.Interfaces.Repository;
using Application.Interfaces.UseCases.Professionals;
using AutoMapper;

namespace Application.UseCases.Professionals
{
    public class DeleteProfessional: IDeleteProfessional
    {
        private IProfessionalRepository _repository;
        private IMapper _mapper;
        public DeleteProfessional(IProfessionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task ExecuteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
