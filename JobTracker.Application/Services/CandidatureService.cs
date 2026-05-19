using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.Services
{
    public class CandidatureService : ICandidatureService
    {
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IMapper _mapper;

        public CandidatureService(ICandidatureRepository candidatureRepository, IRepository<Company> companyRepository, IMapper mapper)
        {
            _candidatureRepository = candidatureRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandidatureReadDto>> GetAllCandidaturesAsync()
        {
            var candidatures = await _candidatureRepository.GetCandidaturesWithCompanyAsync();
            return _mapper.Map<IEnumerable<CandidatureReadDto>>(candidatures);
        }

        public async Task<CandidatureReadDto?> GetCandidatureByIdAsync(int id)
        {
            var candidature = await _candidatureRepository.GetByIdWithCompanyAsync(id);
            if (candidature == null) return null;

            return _mapper.Map<CandidatureReadDto>(candidature);
        }

        public async Task<CandidatureReadDto> CreateCandidatureAsync(CandidatureCreateDto dto)
        {
            // 1. Recherche / Création de l'entreprise à la volée
            var allCompanies = await _companyRepository.GetAllAsync();
            var existingCompany = allCompanies.FirstOrDefault(c =>
                c.Name.Trim().ToLower() == dto.CompagnyName.Trim().ToLower());

            Company companyToLink;

            if (existingCompany != null)
            {
                companyToLink = existingCompany;
            }
            else
            {
                companyToLink = new Company { Name = dto.CompagnyName.Trim() };
                await _companyRepository.AddAsync(companyToLink);
                await _companyRepository.SaveChangesAsync();
            }

            // 2. Mapping et liaison
            var entity = _mapper.Map<Candidature>(dto);
            entity.DateCandidature = DateTime.UtcNow;
            entity.CompanyId = companyToLink.Id;

            // 3. Sauvegarde de la candidature
            await _candidatureRepository.AddAsync(entity);
            await _candidatureRepository.SaveChangesAsync();

            // 4. Préparation du DTO de retour
            entity.Company = companyToLink;
            return _mapper.Map<CandidatureReadDto>(entity);
        }

        public async Task<bool> UpdateCandidatureAsync(int id, CandidatureUpdateDto updateDto)
        {
            var candidatureFromRepo = await _candidatureRepository.GetByIdAsync(id);
            if (candidatureFromRepo == null) return false;

            // AutoMapper applique les modifications du DTO sur l'entité existante
            _mapper.Map(updateDto, candidatureFromRepo);

            _candidatureRepository.Update(candidatureFromRepo);
            await _candidatureRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCandidatureAsync(int id)
        {
            var toDelete = await _candidatureRepository.GetByIdAsync(id);
            if (toDelete == null) return false;

            await _candidatureRepository.DeleteAsync(toDelete);
            await _candidatureRepository.SaveChangesAsync();

            return true;
        }
    }
}
