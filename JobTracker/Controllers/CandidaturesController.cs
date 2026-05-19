using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidaturesController : ControllerBase
{
    private readonly ICandidatureService _candidatureService;

    //ConnectionsDependencyInjectionExtensions de dependance
    public CandidaturesController(ICandidatureService candidatureService)
    {
       _candidatureService = candidatureService;
    }

    // 1. Lire toutes les candidatures
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidatureReadDto>>> GetAll()
    {
        var candidatures = await _candidatureService.GetAllCandidaturesAsync();
        return Ok(candidatures);
    }

    // 2. Lire une seule candidature par son ID
    [HttpGet("{id}")]
    public async Task<ActionResult<CandidatureReadDto>> GetById(int id)
    {
        var candidature = await _candidatureService.GetCandidatureByIdAsync(id);
        if (candidature == null)
            return NotFound();
        return Ok(candidature);
    }

    // 3. Créer une nouvelle candidature
    [HttpPost]
    public async Task<ActionResult<CandidatureReadDto>> Create(CandidatureCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CompagnyName))
        {
            return BadRequest("Le nom de l'entreprise est obligatoire.");
        }

        var result = await _candidatureService.CreateCandidatureAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    // 4. Modifier une candidature
    // 4. Modifier une candidature
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CandidatureUpdateDto updateDto)
    {

        var isUpdated = await _candidatureService.UpdateCandidatureAsync(id, updateDto);

        if (!isUpdated) return NotFound();

        return NoContent();
    }

    // 5. Supprimer une candidature
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await _candidatureService.DeleteCandidatureAsync(id);

        if (!isDeleted) return NotFound();

        return NoContent();
    }
}

