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
    private readonly ICandidatureRepository _candidatureRepository;
    private readonly IRepository<Company> _companyRepository;
    private readonly IMapper _mapper;

    //ConnectionsDependencyInjectionExtensions de dependance
    public CandidaturesController(ICandidatureRepository candidatureRepository, IRepository<Company> companyRepository, IMapper mapper)
    {
        _candidatureRepository = candidatureRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    // 1. Lire toutes les candidatures
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidature>>> GetAll()
    {
        var candidatures = await _candidatureRepository.GetCandidaturesWithCompanyAsync();
        return Ok(_mapper.Map<IEnumerable<CandidatureReadDto>>(candidatures));
    }

    // 2. Lire une seule candidature par son ID
    [HttpGet("{id}")]
    public async Task<ActionResult<CandidatureReadDto>> GetById(int id)
    {
        var candidature = await _candidatureRepository.GetByIdWithCompanyAsync(id);
        if (candidature == null)
            return NotFound();
        return Ok(_mapper.Map<CandidatureReadDto>(candidature));
    }

    // 3. Créer une nouvelle candidature
    [HttpPost]
    public async Task<ActionResult<CandidatureReadDto>> Create(CandidatureCreateDto dto)   // On change le type de retour pour refléter qu'on renvoie un DTO
    {
        // 1. Sécurité : On s'assure qu'un nom d'entreprise a été fourni
        if (string.IsNullOrWhiteSpace(dto.CompagnyName))
        {
            return BadRequest("Le nom de l'entreprise est obligatoire.");
        }

        // A. GESTION DE L'ENTREPRISE (La nouveauté)
        // On va chercher si l'entreprise existe déjà dans la table des Companies
        var allCompagny = await _companyRepository.GetAllAsync();
        var existingCompany = allCompagny.FirstOrDefault(c =>
          c.Name.Trim().ToLower() == dto.CompagnyName.Trim().ToLower());
        Company companyToLink;

        if (existingCompany != null)
        {
            // Si elle existe, on va utiliser celle-là
            companyToLink = existingCompany;
        }
        else
        {
            // Si elle n'existe pas, on la crée à la volée dans sa propre table
            companyToLink = new Company { Name = dto.CompagnyName.Trim() };
            await _companyRepository.AddAsync(companyToLink);
            await _companyRepository.SaveChangesAsync(); // Sauvegarde pour générer son ID
        }

        // B. CONVERSION DU DTO EN ENTITÉ (Ton code d'origine adapté)
        var entity = _mapper.Map<Candidature>(dto);
        entity.DateCandidature = DateTime.Now;

        // C. LIAISON (On associe l'ID de la compagnie qu'on vient de trouver/créer)
        entity.CompanyId = companyToLink.Id;

        // D. SAUVEGARDE DE LA CANDIDATURE (Ton code d'origine)
        await _candidatureRepository.AddAsync(entity);
        await _candidatureRepository.SaveChangesAsync();

        // On force la propriété de navigation pour le mapping de sortie
        entity.Company = companyToLink;

        // E. RETOUR (Ton code d'origine)
        var readDto = _mapper.Map<CandidatureReadDto>(entity);
        return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
    }


    // 4. Modifier une candidature
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CandidatureUpdateDto updateDto)
    {
        var candidatureFromRepo = await _candidatureRepository.GetByIdAsync(id);
        if (candidatureFromRepo == null) return NotFound();

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

