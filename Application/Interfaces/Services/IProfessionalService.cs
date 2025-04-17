using Application.DTOs.Availibility;
using Application.DTOs.Customer;
using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProfessionalService
    {
        Task<ProfessionalDTO> Create(CreateProfessionalDTO createProfessionalDTO);
        Task<ProfessionalDTO> Update(Guid id, UpdateProfessionalDTO updateProfessionalDTO);
        Task<ProfessionalDTO> GetById(Guid id);
        Task<ProfessionalDTO> GetByEmail(string email);
        Task<ProfessionalInternalDTO> GetByEmailInternal(string email);
        Task<IEnumerable<CustomerShortDTO>> GetCustomersAsync(Guid professionalId);
        Task Delete(Guid id);
    }

}
