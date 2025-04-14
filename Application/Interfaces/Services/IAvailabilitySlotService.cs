using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAvailabilitySlotService
    {
        Task<AvailabilitySlotDTO> Create(CreateAvailabilitySlotDTO availabilityDTO);
        Task<AvailabilitySlotDTO> Update(Guid id, UpdateAvailabilitySlotDTO availabilityDTO);
        Task Delete(Guid id);

    }
}
