using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;
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

        public async Task<IEnumerable<InteractionReadDto>> GetAllInteractionsAsync()
        {
            var interactions = await _interactionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InteractionReadDto>>(interactions);
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
            // 1. SÉCURITÉ : On vérifie si la candidature existe AVANT de créer l'interaction
            var candidature = await _candidatureRepository.GetByIdAsync(dto.CandidatureId);

            if (candidature == null)
            {
                // On lève une exception précise. 
                // KeyNotFoundException est parfaite pour dire "Ressource introuvable" (404)
                throw new KeyNotFoundException($"La candidature avec l'ID {dto.CandidatureId} n'existe pas.");
            }

            // 2. Si elle existe, on continue le process normal
            var interaction = _mapper.Map<Interaction>(dto);
            interaction.CreatedAt = DateTime.UtcNow;

            await _interactionRepository.AddAsync(interaction);
            await _interactionRepository.SaveChangesAsync();

            // 3. Logique de mise à jour automatique (déjà sécurisée puisque candidature n'est plus nulle !)
            switch (interaction.Type)
            {
                case TypeInteraction.AppelRh:
                case TypeInteraction.Entretiens:
                case TypeInteraction.EntretienTechnique:
                case TypeInteraction.EntretienFinal:
                    candidature.Status = JobStatus.Entretien;
                    break;

                case TypeInteraction.Refus:
                    candidature.Status = JobStatus.Refusé;
                    break;

                case TypeInteraction.OffreRecu:
                    candidature.Status = JobStatus.Accepté;
                    break;

                default:
                    break;
            }

            _candidatureRepository.Update(candidature);
            await _candidatureRepository.SaveChangesAsync();

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