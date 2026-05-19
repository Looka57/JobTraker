using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // Remarque : J'ai gardé celle-ci car elle correspond à ton interface
        public async Task<IEnumerable<CompanyReadDto>> GetAllCompaniesAsync()
        {
            // On attend (await) les données de la BDD avant de mapper
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyReadDto>>(companies);
        }

        public async Task<CompanyReadDto?> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null) return null;

            return _mapper.Map<CompanyReadDto>(company);
        }

        public async Task<CompanyReadDto> CreateCompanyAsync(CompanyCreateDto dto)
        {
            var company = _mapper.Map<Company>(dto);

            await _companyRepository.AddAsync(company);
            await _companyRepository.SaveChangesAsync(); // Important pour générer l'ID !

            return _mapper.Map<CompanyReadDto>(company);
        }

        public async Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDto updateDto)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);
            if (existingCompany == null) return false;

            _mapper.Map(updateDto, existingCompany);

            // Correction ici : On appelle .Update() (sans async/await car EF Core fait le tracking en synchrone)
            _companyRepository.Update(existingCompany);

            // C'est le SaveChanges qui persiste de manière asynchrone
            await _companyRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);
            if (existingCompany == null) return false;

            await _companyRepository.DeleteAsync(existingCompany);
            await _companyRepository.SaveChangesAsync();

            return true;
        }
    }
}