using JobTracker.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTracker.Domain.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        // Récupère toutes les entreprises (si besoin d'un comportement spécifique)
        // Note : Si GetAllAsync() du repo générique te suffit, tu peux retirer cette ligne.
        Task<IEnumerable<Company>> GetCompaniesAsync();

        // Récupère une entreprise par son ID en incluant toutes ses candidatures liées !
        Task<Company?> GetCompanyWithCandidaturesAsync(int id);
    }
}