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
        Task Delete(Guid id);
    }

}
