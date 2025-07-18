using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.AvailabilitySlots
{
    public interface IGetAllAvailabilitySlotsByProfessionalAndDay
    {
        Task<IEnumerable<AvailabilitySlot>> Execute(Guid professionalId, DateTime date);
    }
}
