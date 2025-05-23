﻿using Application.DTOs.Customer;
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
    public class CustomerProfile : Profile
    {
        public CustomerProfile() { 
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<UpdateCustomerDTO, Customer>();
            CreateMap<CreateCustomerDTO, Customer>();

            CreateMap<Customer, CustomerShortDTO>();
            CreateMap<Customer, CustomerInternalDTO>();
            CreateMap<Professional, ProfessionalShortDTO>();

        }
    }
}
