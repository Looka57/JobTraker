using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Infrastructure.Repositories
{
    public class CandidatureRepository : Repository<Candidature>, ICandidatureRepository
    {
        public CandidatureRepository(JobTrackDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Candidature>> GetCandidaturesWithCompanyAsync()
        {
            // On utilise le mot-clé .Include pour forcer le chargement de la jointure SQL
            return await _context.Candidatures
                .Include(c => c.Company)
                .ToListAsync();
        }

        public async Task<Candidature?> GetByIdWithCompanyAsync(int id)
        {
            return await _context.Candidatures
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
