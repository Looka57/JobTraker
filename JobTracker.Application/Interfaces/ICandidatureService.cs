using JobTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.Interfaces
{
    public interface ICandidatureService
    {
        Task<IEnumerable<CandidatureReadDto>> GetAllCandidaturesAsync();
        Task<CandidatureReadDto?> GetCandidatureByIdAsync(int id);
        Task<CandidatureReadDto> CreateCandidatureAsync(CandidatureCreateDto dto);
        Task<bool> UpdateCandidatureAsync(int id, CandidatureUpdateDto updateDto);
        Task<bool> DeleteCandidatureAsync(int id);
    }
}
