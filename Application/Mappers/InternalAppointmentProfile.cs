using Application.DTOs.Appointment;
using Application.DTOs.Client;
using Application.DTOs.Professional;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class InternalAppointmentProfile : Profile
    {
        public InternalAppointmentProfile() {
            CreateMap<InternalAppointmentDTO, Appointment>();
            CreateMap<InternalAppointment, InternalAppointmentDTO>();
            CreateMap<CreateInternalAppointmentDTO, InternalAppointment>();
            CreateMap<UpdateInternalAppointmentDTO, InternalAppointment>();

        }
    }
}
