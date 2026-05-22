using JobTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.Interfaces
{
    public interface IInteractionService
    {
        Task<IEnumerable<InteractionReadDto>> GetInteractionsByCandidatureAsync(int candidatureId);
        Task<InteractionReadDto?> GetInteractionByIdAsync(int id);
        Task<InteractionReadDto> CreateInteractionAsync(InteractionCreateDto dto);
        Task<bool> UpdateInteractionAsync(int id, InteractionUpdateDto dto);
        Task<bool> DeleteInteractionAsync(int id);
    }
}
