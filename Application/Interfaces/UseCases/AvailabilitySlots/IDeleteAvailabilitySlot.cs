using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.AvailabilitySlots
{
    public interface IDeleteAvailabilitySlot
    {
        Task ExecuteAsync(Guid id);
    }
}
