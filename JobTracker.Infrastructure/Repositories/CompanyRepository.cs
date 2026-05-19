using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        // Correction ici : Le constructeur s'appelle bien CompanyRepository !
        public CompanyRepository(JobTrackDbContext context) : base(context)
        {
        }

        // Implémentation de la méthode pour récupérer toutes les entreprises
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Compagny.ToListAsync();
        }

        // Implémentation de la méthode pour récupérer l'entreprise avec ses candidatures
        public async Task<Company?> GetCompanyWithCandidaturesAsync(int id)
        {
            return await _context.Compagny
                .Include(c => c.Name) // Charge la liste des candidatures liées à cette boîte
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}