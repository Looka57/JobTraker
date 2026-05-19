using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        // Le contrôleur ne parle qu'au service, respect parfait du pattern !
        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // 1. GET ALL : Récupérer toutes les entreprises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyReadDto>>> GetAll()
        {
            var result = await _companyService.GetAllCompaniesAsync();
            return Ok(result);
        }

        // 2. GET BY ID : Récupérer une entreprise par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyReadDto>> GetById(int id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // 3. POST : Créer une entreprise manuellement
        [HttpPost]
        public async Task<ActionResult<CompanyReadDto>> Create(CompanyCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Le nom de l'entreprise est obligatoire.");
            }

            var result = await _companyService.CreateCompanyAsync(dto);

            // Permet de renvoyer un statut 201 Created avec l'URL pour voir l'élément créé
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // 4. PUT : Modifier les infos d'une entreprise (Site, Lieu, etc.)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CompanyUpdateDto updateDto)
        {
            var isUpdated = await _companyService.UpdateCompanyAsync(id, updateDto);

            if (!isUpdated) return NotFound();

            return NoContent(); // Statut 204 standard pour une mise à jour réussie
        }

        // 5. DELETE : Supprimer une entreprise
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _companyService.DeleteCompanyAsync(id);

            if (!isDeleted) return NotFound();

            return NoContent(); // Statut 204 standard pour une suppression réussie
        }
    }
}