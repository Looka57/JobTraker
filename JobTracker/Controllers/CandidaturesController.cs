using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidaturesController : ControllerBase
{
    private readonly IRepository<Candidature> _candidatureRepository;

    //ConnectionsDependencyInjectionExtensions de dependance
    public CandidaturesController(IRepository<Candidature> candidatureRepository)
    {
        _candidatureRepository = candidatureRepository;
    }

    // 1. Lire toutes les candidatures
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidature>>> GetAll()
    {
        var candidatures = await _candidatureRepository.GetAllAsync();
        return Ok(candidatures);
    }

    // 2. Lire une seule candidature par son ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Candidature>> GetById(int id)
    {
        var candidature = await _candidatureRepository.GetByIdAsync(id);
        if (candidature == null)
            return NotFound();
        return Ok(candidature);
    }

    // 3. Créer une nouvelle candidature
    [HttpPost]
    public async Task<ActionResult<Candidature>> Create(Candidature candidature)
    {
        await _candidatureRepository.AddAsync(candidature);
        await _candidatureRepository.SaveChangesAsync();
        // Retourne un code 201 avec le lien vers l'objet créé
        return CreatedAtAction(nameof(GetById), new { id = candidature.Id }, candidature);
    }

    // 4. Modifier une candidature
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Candidature candidature)
    {
        if (id != candidature.Id) return BadRequest();

        _candidatureRepository.Update(candidature);
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

