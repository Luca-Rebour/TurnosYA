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
    public class ClientProfile : Profile
    {
        public ClientProfile() { 
            CreateMap<ClientDTO, Client>();
            CreateMap<Client, ClientDTO>();
            CreateMap<UpdateClientDTO, Client>();
            CreateMap<CreateClientDTO, Client>();

            CreateMap<Client, ClientShortDTO>();
            CreateMap<Client, ClientInternalDTO>();
            CreateMap<Professional, ProfessionalShortDTO>();

        }
    }
}
