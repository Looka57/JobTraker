using AutoMapper;
using JobTracker.Application.DTOs;
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
    private readonly IRepository<Candidature> _candidatureRepository;
    private readonly IMapper _mapper;

    //ConnectionsDependencyInjectionExtensions de dependance
    public CandidaturesController(IRepository<Candidature> candidatureRepository, IMapper mapper)
    {
        _candidatureRepository = candidatureRepository;
        _mapper = mapper;
    }

    // 1. Lire toutes les candidatures
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidature>>> GetAll()
    {
        var candidatures = await _candidatureRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CandidatureReadDto>>(candidatures));
    }

    // 2. Lire une seule candidature par son ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Candidature>> GetById(int id)
    {
        var candidature = await _candidatureRepository.GetByIdAsync(id);
        if (candidature == null)
            return NotFound();
        return Ok(_mapper.Map<IEnumerable<CandidatureReadDto>>(candidature));
    }

    // 3. Créer une nouvelle candidature
    [HttpPost]
    public async Task<ActionResult<CandidatureReadDto>> Create(CandidatureCreateDto dto)   // On change le type de retour pour refléter qu'on renvoie un DTO
    {
        // A. Convertir le DTO reçu en Entité (ce qui va en base)
        var entity = _mapper.Map<Candidature>(dto);  // On utilise 'dto' qui est l'objet reçu en paramètre   
        entity.DateCandidature = DateTime.Now; // Ajout de la logique métier sur l'entité

        // B. Sauvegarder via le repository
        await _candidatureRepository.AddAsync(entity);
        await _candidatureRepository.SaveChangesAsync();

        // C. Convertir l'entité sauvegardée (qui a maintenant un ID) en ReadDto
        var readDto = _mapper.Map<CandidatureReadDto>(entity);
        return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);// On renvoie le DTO de lecture au client
    }


    // 4. Modifier une candidature
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CandidatureUpdateDto updateDto)
    {
        var candidatureFromRepo = await _candidatureRepository.GetByIdAsync(id);
        if (candidatureFromRepo == null ) return NotFound();

        // On applique les modifications du DTO sur l'entité existante
        _mapper.Map(updateDto, candidatureFromRepo); // Cette ligne met à jour 'candidatureFromRepo' avec les valeurs de 'updateDto'
        _candidatureRepository.Update(candidatureFromRepo);
        await _candidatureRepository.SaveChangesAsync();
        return NoContent();

    }

    // 5. Supprimer une candidature
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var toDeleted = await _candidatureRepository.GetByIdAsync(id);
        if (toDeleted == null) return NotFound();

        await _candidatureRepository.DeleteAsync(toDeleted);
        await _candidatureRepository.SaveChangesAsync();

        return NoContent();
    }
}

