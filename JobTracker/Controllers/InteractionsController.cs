using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteractionsController : ControllerBase
    {
        private readonly IInteractionService _interactionService;

        public InteractionsController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }


        // 1. Récupérer TOUTES les interactions globales (Ajouté/Corrigé)
        // URL: GET /api/interactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InteractionReadDto>>> GetAll()
        {
            var result = await _interactionService.GetAllInteractionsAsync(); // À vérifier si elle existe dans ton service
            return Ok(result);
        }

        // Récupérer toutes les interactions d'une candidature spécifique
        [HttpGet("candidature/{candidatureId}")]
        public async Task<ActionResult<IEnumerable<InteractionReadDto>>> GetByCandidature(int candidatureId)
        {
            var result = await _interactionService.GetInteractionsByCandidatureAsync(candidatureId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<InteractionReadDto>> Create(InteractionCreateDto dto)
        {
            var result = await _interactionService.CreateInteractionAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InteractionReadDto>> GetById(int id)
        {
            var result = await _interactionService.GetInteractionByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

       

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InteractionUpdateDto dto)
        {
            var success = await _interactionService.UpdateInteractionAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _interactionService.DeleteInteractionAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}