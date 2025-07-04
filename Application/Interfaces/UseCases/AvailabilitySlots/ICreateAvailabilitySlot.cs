using Application.DTOs.Availability;
using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.AvailabilitySlots
{
    public interface ICreateAvailabilitySlot
    {
        Task<ProfessionalDTO> ExecuteAsync(CreateAvailabilitySlotDTO dto);
    }
}
