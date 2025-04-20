using Application.DTOs.Appointment;
using Application.DTOs.UserActivity;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UserActivityProfile: Profile
    {
        public UserActivityProfile()
        {
            CreateMap<UserActivityDTO, UserActivity>();
            CreateMap<UserActivity, UserActivityDTO>();



        }
    }
}
