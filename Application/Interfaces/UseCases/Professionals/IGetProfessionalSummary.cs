﻿using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Professionals
{
    public interface IGetProfessionalSummary
    {
        Task<SummaryDataDTO> ExecuteAsync(Guid professionalId);
    }
}
