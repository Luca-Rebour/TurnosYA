using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAvailabilitySlotService
    {
        Task<ProfessionalDTO> Create(CreateAvailabilitySlotDTO availabilityDTO);
        Task<ProfessionalDTO> Update(Guid id, UpdateAvailabilitySlotDTO availabilityDTO);
        Task Delete(Guid id);

    }
}
