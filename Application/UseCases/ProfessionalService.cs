using Aplication.Interfaces.Repository;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public ProfessionalService(IProfessionalRepository repository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }


        public async Task<ProfessionalDTO> Create(CreateProfessionalDTO createProfessionalDTO)
        {
            string password = _passwordHasher.Hash(createProfessionalDTO.Password);

            Professional professional = _mapper.Map<Professional>(createProfessionalDTO);
            professional.SetPassword(password);

            await _repository.AddAsync(professional);
            return _mapper.Map<ProfessionalDTO>(professional);
        }

        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<ProfessionalDTO> GetById(Guid id)
        {
            Professional professional = await _repository.GetByIdAsync(id);

            if (professional == null)
            {
                throw new Exception("Profesional no encontrado");
            }

            return _mapper.Map<ProfessionalDTO>(professional);
        }


        public async Task<ProfessionalDTO> Update(Guid id, UpdateProfessionalDTO dto)
        {
            // 1. Traer el profesional existente
            Professional professional = await _repository.GetByIdAsync(id);
            if (professional == null)
            {
                throw new Exception("Profesional no encontrado");
            }

            // 2. Mapear sobre la instancia existente
            _mapper.Map(dto, professional); // AutoMapper actualiza las propiedades

            // 3. Guardar cambios
            await _repository.UpdateAsync(id, professional);

            // 4. Devolver el DTO actualizado
            return _mapper.Map<ProfessionalDTO>(professional);
        }

        public async Task<ProfessionalDTO> GetByEmail(string email)
        {
            Professional professional = await _repository.GetByMailAsync(email);

            if (professional == null)
            {
                throw new Exception("Profesional no encontrado");
            }

            return _mapper.Map<ProfessionalDTO>(professional);
        }
        public async Task<ProfessionalInternalDTO> GetByEmailInternal(string email)
        {
            Professional professional = await _repository.GetByMailAsync(email);


            return _mapper.Map<ProfessionalInternalDTO>(professional);
        }

    }

}
