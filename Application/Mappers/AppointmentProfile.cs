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
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile() {
            CreateMap<AppointmentDTO, Appointment>();
            CreateMap<Appointment, AppointmentDTO>();
            CreateMap<CreateAppointmentDTO, Appointment>();
            CreateMap<UpdateAppointmentDTO, Appointment>();

        }
    }
}
