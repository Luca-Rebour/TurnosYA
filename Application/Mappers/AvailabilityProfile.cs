using Application.DTOs.Appointment;
using Application.DTOs.Availibility;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class AvailabilityProfile : Profile
    {
        public AvailabilityProfile()
        {
            CreateMap<AvailabilitySlotDTO, AvailabilitySlot>();
            CreateMap<AvailabilitySlot, AvailabilitySlotDTO>();
        }
    }
}
