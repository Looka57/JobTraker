using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTracker.Application.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly IInteractionRepository _interactionRepository;
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IMapper _mapper;

        public InteractionService(
            IInteractionRepository interactionRepository,
            ICandidatureRepository candidatureRepository,
            IMapper mapper)
        {
            _interactionRepository = interactionRepository;
            _candidatureRepository = candidatureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InteractionReadDto>> GetInteractionsByCandidatureAsync(int candidatureId)
        {
            var interactions = await _interactionRepository.GetInteractionsByCandidatureIdAsync(candidatureId);
            return _mapper.Map<IEnumerable<InteractionReadDto>>(interactions);
        }

        public async Task<InteractionReadDto?> GetInteractionByIdAsync(int id)
        {


            var interaction = await _interactionRepository.GetByIdAsync(id);
            if (interaction == null) return null;

            return _mapper.Map<InteractionReadDto>(interaction);
        }

        public async Task<InteractionReadDto> CreateInteractionAsync(InteractionCreateDto dto)
        {
            var interaction = _mapper.Map<Interaction>(dto);

            interaction.CreatedAt = DateTime.UtcNow; // On force la date du jour

            await _interactionRepository.AddAsync(interaction);
            await _interactionRepository.SaveChangesAsync();

            return _mapper.Map<InteractionReadDto>(interaction);
        }

        public async Task<bool> UpdateInteractionAsync(int id, InteractionUpdateDto dto)
        {
            var existing = await _interactionRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            _interactionRepository.Update(existing);
            await _interactionRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteInteractionAsync(int id)
        {
            var existing = await _interactionRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _interactionRepository.DeleteAsync(existing);
            await _interactionRepository.SaveChangesAsync();

            return true;
        }
    }
}