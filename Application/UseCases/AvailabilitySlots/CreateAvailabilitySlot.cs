using Aplication.Interfaces.Repository;
using Application.DTOs.Availability;
using Application.DTOs.Professional;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.AvailabilitySlots;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.AvailabilitySlots
{
    public class CreateAvailabilitySlot: ICreateAvailabilitySlot
    {
        private IAvailabilitySlotRepository _repository;
        private IProfessionalRepository _professionalRepository;
        private IMapper _mapper;
        public CreateAvailabilitySlot(IAvailabilitySlotRepository repository, IProfessionalRepository professionalRepository, IMapper mapper)
        {
            _repository = repository;
            _professionalRepository = professionalRepository;
            _mapper = mapper;
        }
        public async Task<ProfessionalDTO> ExecuteAsync(CreateAvailabilitySlotDTO dto)
        {
            dto.Validate();

            Professional professional = await _professionalRepository.GetByIdAsync(dto.ProfessionalId);

            var slotDuration = TimeSpan.FromMinutes(30);
            var currentDate = dto.StartDate;

            // Avanzar hasta el proximo dia coincidente con dto.DayOfWeek
            while (currentDate.DayOfWeek != dto.DayOfWeek)
                currentDate = currentDate.AddDays(1);

            var slots = new List<AvailabilitySlot>();

            for (int week = 0; week < dto.Weeks; week++)
            {
                var targetDate = currentDate.AddDays(week * 7);
                var currentTime = dto.StartTime;

                while (currentTime + slotDuration <= dto.EndTime)
                {
                    var slot = new AvailabilitySlot(
                        date: targetDate.Date,
                        dayOfWeek: dto.DayOfWeek,
                        startTime: currentTime,
                        endTime: currentTime + slotDuration,
                        availabilityStatus: AvailabilityStatus.Available
                    );

                    slot.SetProfessional(professional);
                    slots.Add(slot);

                    currentTime += slotDuration;
                }
            }

            await _repository.AddRangeAsync(slots);

            return _mapper.Map<ProfessionalDTO>(professional);
        }


    }
}
