using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.AvailabilitySlots
{
    public interface IUpdateAvailabilitySlot
    {
        Task<ProfessionalDTO> ExecuteAsync(Guid id, UpdateAvailabilitySlotDTO availabilitySlotDTO);
    }
}
