using JobTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyReadDto>> GetAllCompaniesAsync();
        Task<CompanyReadDto?> GetCompanyByIdAsync(int id);
        Task<CompanyReadDto> CreateCompanyAsync(CompanyCreateDto dto);
        Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDto updateDto);
        Task<bool> DeleteCompanyAsync(int id);
    }
}
