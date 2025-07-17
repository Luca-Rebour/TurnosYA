using Application.DTOs.Appointment;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    internal class ExternalAppointmentProfile : Profile
    {
        public ExternalAppointmentProfile()
        {
            CreateMap<ExternalAppointmentDTO, Appointment>();
            CreateMap<ExternalAppointment, ExternalAppointmentDTO>();
            CreateMap<CreateExternalAppointmentDTO, ExternalAppointment>();
            CreateMap<UpdateExternalAppointmentDTO, ExternalAppointment>();

        }
    }
}
