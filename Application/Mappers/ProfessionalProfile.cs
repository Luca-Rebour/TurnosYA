using Application.DTOs.Professional;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Mappers
{
    public class ProfessionalProfile : Profile
    {
        public ProfessionalProfile()
        {
            CreateMap<CreateProfessionalDTO, Professional>();
            CreateMap<Professional, ProfessionalDTO>();
            CreateMap<UpdateProfessionalDTO, Professional>();
            CreateMap<Professional, ProfessionalInternalDTO>();

        }
    }
}
