using Application.DTOs.Customer;
using Application.DTOs.ExternalCustomer;
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
    public class ExternalCustomerProfile: Profile
    {
        public ExternalCustomerProfile()
        {
            CreateMap<ExternalCustomer, CreateExternalCustomerDTO>();
            CreateMap<CreateExternalCustomerDTO, ExternalCustomer>();
            CreateMap<ExternalCustomer, ExternalCustomerDTO>();
            CreateMap<ExternalCustomer, CustomerShortDTO>();
        }
    }
}
