using Aplication.Interfaces.Repository;
using Application.Interfaces.UseCases.AvailabilitySlots;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.AvailabilitySlots
{
    public class DeleteAvailabilitySlot: IDeleteAvailabilitySlot
    {
        private IAvailabilitySlotRepository _repository;
        public DeleteAvailabilitySlot(IAvailabilitySlotRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
