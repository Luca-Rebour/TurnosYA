using Application.DTOs.Client;
using Application.DTOs.ExternalClient;
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
    public class ExternalClientProfile: Profile
    {
        public ExternalClientProfile()
        {
            CreateMap<ExternalClient, CreateExternalClientDTO>();

        }
    }
}
