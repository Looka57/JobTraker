using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; // Ne pas oublier pour le .Where
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Repositories
{
    public class InteractionRepository : Repository<Interaction>, IInteractionRepository
    {
        public InteractionRepository(JobTrackDbContext context) : base(context)
        {
        }

        // 1. Pour charger TOUTES les interactions avec les détails (Pour ton écran principal "Mes Notes")
        public override async Task<IEnumerable<Interaction>> GetAllAsync()
        {
            return await _context.Interactions
                .Include(i => i.Candidature)
                    .ThenInclude(c => c.Company)
                .ToListAsync();
        }

        // 2. Pour charger les interactions d'une seule candidature spécifique
        public async Task<IEnumerable<Interaction>> GetInteractionsByCandidatureIdAsync(int candidatureId)
        {
            return await _context.Interactions
                .Include(i => i.Candidature)
                    .ThenInclude(c => c.Company)
                .Where(i => i.CandidatureId == candidatureId)
                .ToListAsync();
        }
    }
}